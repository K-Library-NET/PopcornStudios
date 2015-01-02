using System;
using System.Data;
using System.Collections;
using System.Reflection;
using System.Text;
//using System.Windows.Forms;

namespace BLS_AspNet.DALFactory
{

	struct ParamItem
	{
		public string paramName;
		public string placeName;
		public string colName;
		public object Value;
	}

	class DALEngine : IDALEngine   //IDisposable
	{		
		// 
		// private data members
		//
		IDbConnection conn       = null;
		string connectionString  = "";
		ArrayList parameters = new ArrayList();
		ArrayList keyFieldParameters = new ArrayList();
		ArrayList dataFieldParameters = new ArrayList();
		private DataSource _dataSource = null;
		bool canClose = true;
		
		
		public DALEngine( DataSource _ds)
		{
			this._dataSource = _ds;
		}
		
		public bool CanClose
		{
			get { return canClose;  }
			set { canClose = value;  }
		}
		
		public string ConnectionString
		{
			get { return connectionString;  }
			set { connectionString = value; }
		}
		
		protected IDbConnection Connection
		{
			get 
			{ 
				conn = this._dataSource.CreateConnection();
					
				return conn;  
			}
		}
		
		protected ArrayList Parameters
		{
			get { return parameters; }
		}
		

		public void Close()
		{		
			if (CanClose && conn != null)
			{
				conn.Dispose();
				conn = null;			
			}
		}		
	
		
		public void Dispose()
		{
			Close();
		
			GC.SuppressFinalize(this);
		}
	
			
		public DALParameter GetParameter(string name)
		{
			foreach (DALParameter param in Parameters)
			{
				if (param.Name == name)
					return param;
			}
			
			return null;
		}
		
		public void UpdateOutputParameters(IDbCommand cmd)
		{
			int index = 0;
			foreach (DALParameter param in Parameters)
			{
				if (param.Direction == ParameterDirection.Output || param.Direction == ParameterDirection.InputOutput)
				{
					param.Value = ((IDataParameter)cmd.Parameters[index]).Value;
				
				}
				
				index++;
			}			
		
		}
		
		public void AddParameter(DALParameter param)
		{
			Parameters.Add(param);
		}
		
		public void ClearParameters()
		{
			Parameters.Clear();
		}

	
		//
		// Business objects methods
		//
		public object CreateFromReader(IDataReader reader, Type objType)
		{
			object instance = Activator.CreateInstance(objType, true);
			
			PropertyInfo[] properties = objType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
			
			for (int i=0; i < properties.Length; i++)
			{
				BaseFieldAttribute[] fields = (BaseFieldAttribute[])properties[i].GetCustomAttributes(typeof(BaseFieldAttribute), true);

				if (fields.Length > 0 && reader.GetSchemaTable().Columns.Contains(fields[0].ColumnName))
				{
					object val = reader[fields[0].ColumnName];
					
					if (properties[i].PropertyType.IsEnum)
					{
						val = Enum.Parse(properties[i].PropertyType, val.ToString());
					}

					if (reader[fields[0].ColumnName].GetType().Name == "DBNull")
					{
						val = null;
						continue;
					}

					if (val == null && fields[0].Type == "VarChar")
					{
						val = "";
					}

					if (properties[i].PropertyType.ToString().Trim() == "System.Byte[]")
					{
						if (reader[fields[0].ColumnName].GetType().Name == "DBNull")
						{
							val = (byte[])null;
						}
						else
						{
							val = (byte[])reader[fields[0].ColumnName];
						}
					}
					
					try
					{
						properties[i].SetValue(instance, val, null);
					}
					catch(Exception ex)
					{
						throw new Exception("CreateFromReader:" + fields[0].ColumnName + ex.Message + properties[i].PropertyType.ToString() + reader[fields[0].ColumnName].GetType().Name);
					}
				}
			}	
			
			PropertyInfo dbaction = objType.GetProperty("dbaction");
			dbaction.SetValue(instance,"update",null);
						
			return instance;
		
		}
		
		public void CreateFromDataRow(DataRow dataRow, object instance)
		{
			PropertyInfo[] properties = instance.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
			
			for (int i=0; i < properties.Length; i++)
			{
				BaseFieldAttribute[] fields = (BaseFieldAttribute[])properties[i].GetCustomAttributes(typeof(BaseFieldAttribute), true);

				if (fields.Length > 0 && dataRow.Table.Columns.Contains(fields[0].ColumnName))
				{
					object val = dataRow[fields[0].ColumnName];

					if (properties[i].PropertyType.IsEnum)
					{
						val = Enum.Parse(properties[i].PropertyType, val.ToString());
					}

					if (dataRow[fields[0].ColumnName].GetType().Name == "DBNull")
					{
						val = null;
						continue;
					}

					if (val == null && fields[0].Type == "VarChar")
					{
						val = "";
						continue;
					}
					
					try
					{
						if (properties[i].PropertyType.ToString().Trim() == "System.Byte[]")
						{
							if (dataRow[fields[0].ColumnName].GetType().Name == "DBNull")
							{
								properties[i].SetValue(instance, (byte[])null, null);
							}
							else
							{
								properties[i].SetValue(instance, (byte[])dataRow[fields[0].ColumnName], null);
							}
						}
						else
						{
							if (properties[i].PropertyType.ToString().Trim().IndexOf("Int")>=0)// == "System.Int32")
							{
								string s = val.ToString();
								int k = s.IndexOf('.');
								if(k>=0) s = val.ToString().Substring(0,k);
								int vv = int.Parse(s);
								properties[i].SetValue(instance, vv, null);
							}
							else
							{
								properties[i].SetValue(instance, val, null);
							}
						}
					}
					catch(Exception ex)
					{
						throw new Exception("CreateFromDataRow赋值出错:" + fields[0].ColumnName + ex.Message + properties[i].PropertyType.ToString() + "," + dataRow[fields[0].ColumnName].GetType().Name);
					}
				}
			}	
			
			PropertyInfo dbaction = instance.GetType().GetProperty("dbaction");
			if(dataRow.RowState == DataRowState.Added)
				dbaction.SetValue(instance, "insert",null);
			else
				dbaction.SetValue(instance,"update",null);		
		}
		
		private bool ColumnInReader(IDataReader reader, string colname)
		{
			DataTable dt = reader.GetSchemaTable();
			if(dt != null && dt.Rows.Count>0)
			{
				foreach(DataRow row in dt.Rows)
				{
					if(row["ColumnName"].ToString().Trim().ToLower() == colname.Trim().ToLower())
						return true;
				}

			}
			return false;
		}

		public void CreateFromReader(IDataReader reader, object instance)
		{
			PropertyInfo[] properties = instance.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

			for (int i=0; i < properties.Length; i++)
			{
				BaseFieldAttribute[] fields = (BaseFieldAttribute[])properties[i].GetCustomAttributes(typeof(BaseFieldAttribute), true);

				if (fields.Length > 0 && ColumnInReader(reader, fields[0].ColumnName) )
				{
					object val = reader[fields[0].ColumnName];
					
					if (properties[i].PropertyType.IsEnum)
					{
						val = Enum.Parse(properties[i].PropertyType, val.ToString());
					}

					if (reader[fields[0].ColumnName].GetType().Name == "DBNull")
					{
						val = null;
						continue;
					}
					
					if (val == null && fields[0].Type == "VarChar")
					{
						val = "";
					}

					if (properties[i].PropertyType.ToString().Trim() == "System.Byte[]")
					{
						if (reader[fields[0].ColumnName].GetType().Name == "DBNull")
						{
							val = (byte[])null;
						}
						else
						{
							val = (byte[])reader[fields[0].ColumnName];
						}
					}
					else
					{
						if (properties[i].PropertyType.ToString().Trim().IndexOf("Int")>=0)// == "System.Int32")
						{
							string s = val.ToString();
							int k = s.IndexOf('.');
							if(k>=0) s = val.ToString().Substring(0,k);
							val = int.Parse(s);
						}
					}

					try
					{
						properties[i].SetValue(instance, val, null);
					}
					catch(Exception ex)
					{
						throw new Exception("CreateFromReader:" + fields[0].ColumnName + ex.Message + "," + properties[i].PropertyType.ToString() + "," + reader[fields[0].ColumnName].GetType().Name);
					}
				}
			}	
			
			PropertyInfo dbaction = instance.GetType().GetProperty("dbaction");
			dbaction.SetValue(instance,"update",null);		
		}
		

		public object RetrieveObject(object keyValue, Type objType)
		{
			object result = null;
		
			DataTableAttribute[] dataTables = (DataTableAttribute[])objType.GetCustomAttributes(typeof(DataTableAttribute), true);
			
			if (dataTables.Length > 0)
			{
			
				PropertyInfo[] properties = objType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
				string clause = "";

				for (int i=0; i < properties.Length; i++)
				{
					KeyFieldAttribute[] keys = (KeyFieldAttribute[])properties[i].GetCustomAttributes(typeof(KeyFieldAttribute), false);

					if (keys.Length > 0)
					{
						if (properties[i].PropertyType == typeof(string) || properties[i].PropertyType == typeof(char))
						{
							clause = keys[0].ColumnName + " = '" + keyValue + "'";
						}
						else
						{
							clause = keys[0].ColumnName + " = " + keyValue;
						}

						break;
					}
				}


				StringBuilder sb = new StringBuilder("SELECT ");
				for (int i=0; i < properties.Length; i++)
				{
					BaseFieldAttribute[] fields = (BaseFieldAttribute[])properties[i].GetCustomAttributes(typeof(BaseFieldAttribute), true);
					
					if (fields.Length > 0)
					{											
						//sb.Append("[");
						sb.Append(fields[0].ColumnName);
						sb.Append(", ");
					}
				}
				
				// remove the last ','
				sb.Remove(sb.Length - 2, 2);				

				sb.Append(" FROM ");
				sb.Append(dataTables[0].TableName);
				sb.Append(" ");
				sb.Append("WHERE ");
				sb.Append(clause);
				
				// ExecQuery_DataReader(sb.ToString());						
                //IDataCommand cmd = this._dataSource.GetTxtCommand(sb.ToString());
                IDataReader reader = this._dataSource.ExecuteTxtReader(sb.ToString());// cmd.ExecuteReader();
				
				try
				{
					if (reader.Read())
					{
						result = CreateFromReader(reader, objType);
					}
				}
				catch (Exception e)
				{
					throw new DALException("Failed to retrieve object [" + objType + "] with sql " + sb.ToString() + e.Message, e);
				}
				finally
				{
					if (reader != null)
					{
						reader.Dispose();
					}
				}				
			}
			else
			{
				throw new ArgumentException("The DataTable attribute wasn't found in the object [keyValue parameter]");
			}
			
			return result;		
		}


		public void RetrieveObject(object o)
		{
			Type objType = o.GetType();
		
			DataTableAttribute[] dataTables = (DataTableAttribute[])objType.GetCustomAttributes(typeof(DataTableAttribute), true);
			
			if (dataTables.Length > 0)
			{
			
				PropertyInfo[] properties = objType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
				StringBuilder cb = new StringBuilder();
				string clause= "";
				int k = 0;

				for (int i=0; i < properties.Length; i++)
				{
					KeyFieldAttribute[] keys = (KeyFieldAttribute[])properties[i].GetCustomAttributes(typeof(KeyFieldAttribute), false);

					if (keys.Length > 0)
					{
						cb.Append(keys[0].ColumnName);
						cb.Append(" = ");
						k++;
						
						if (properties[i].PropertyType == typeof(string) || properties[i].PropertyType == typeof(char))
						{
							string value = properties[i].GetValue(o, null).ToString();
							if (value != "")
							{
								cb.Append("'" + value + "' and ");
							}
							else
							{
								throw new Exception("Primary key字段" + keys[0].ColumnName + "不能为空值！！！");
							}
						}
						else
						{
							string value = properties[i].GetValue(o, null).ToString();
							if (value != "")
							{
								cb.Append( value + " and ");
							}
							else
							{
								throw new Exception("Primary key字段" + keys[0].ColumnName + "不能为空值！！！");
							}
						}
					}
				}
				if(k == 0) throw new Exception("数据库表" + dataTables[0].TableName + "没有 Primary key字段！！！");
				cb.Remove(cb.Length - 4, 4);
				clause = cb.ToString();


				StringBuilder sb = new StringBuilder("SELECT ");
				for (int i=0; i < properties.Length; i++)
				{
					BaseFieldAttribute[] fields = (BaseFieldAttribute[])properties[i].GetCustomAttributes(typeof(BaseFieldAttribute), true);
					
					if (fields.Length > 0)
					{											
						//sb.Append("[");
						sb.Append(fields[0].ColumnName);
						sb.Append(", ");
					}
				}
				
				// remove the last ','
				sb.Remove(sb.Length - 2, 2);				

				sb.Append(" FROM ");
				sb.Append(dataTables[0].TableName);
				sb.Append(" ");
				sb.Append("WHERE ");
				sb.Append(clause);
				
				//MessageBox.Show(sb.ToString());						
                //IDataCommand cmd = this._dataSource.GetTxtCommand(sb.ToString());
                IDataReader reader = this._dataSource.ExecuteTxtReader(sb.ToString());// cmd.ExecuteReader();
				
				try
				{
					if (reader.Read())
					{
						//MessageBox.Show("dfdf");
						CreateFromReader(reader, o);
					}
				}
				catch (Exception e)
				{
					throw new DALException("Failed to retrieve object [" + objType + "] with sql " + sb.ToString() + e.Message, e);
				}
				finally
				{
					if (reader != null)
					{
						reader.Dispose();
					}
				}				
			}
			else
			{
				throw new ArgumentException("The DataTable attribute wasn't found in the object [keyValue parameter]");
			}			
		}

		void GetParameterName(string colname, out ParamItem item)
		{
			string conn = this._dataSource.conn;

			item.paramName = "@" + colname.Trim();
			item.placeName = "?";
			item.Value = null;
			item.colName = colname.Trim();
			switch(conn)
			{
				case "SqlConnection":
					item.placeName = "@" + colname.Trim();
					break;
				case "OracleConnection":
					item.placeName = ":" + colname.Trim();
					item.paramName = colname.Trim();
					break;
			}
		}
																		 
		
		public void UpdateObjectSql(object o, DataTableAttribute dataTable)
		{
			//DALQueryBuilder qBuilder = null;
			ArrayList dataParams = new ArrayList();
			ArrayList keyParams = new ArrayList();

			this.keyFieldParameters.Clear();
			this.dataFieldParameters.Clear();

			PropertyInfo dbaction = o.GetType().GetProperty("dbaction");
			string action = dbaction.GetValue(o, null).ToString();
			
			PropertyInfo[] properties = o.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
			
			for (int i=0; i < properties.Length; i++)
			{
				DataFieldAttribute[] fields = (DataFieldAttribute[])properties[i].GetCustomAttributes(typeof(DataFieldAttribute), true);
				
				if (fields.Length > 0)
				{
					object value = properties[i].GetValue(o, null);

					if (value == null) continue;

					if (value.GetType().IsEnum)
					{
						value = Convert.ToInt32(value);
					}

//					if (value.ToString().Length==0) continue;
		
					ParamItem item;
					this.GetParameterName(fields[0].ColumnName,out item);
					DALParameter param = new DALParameter(item.paramName, value);
			
					dataParams.Add(item);
					param.Type = fields[0].Type;
                   
					if (fields[0].Size != 0)
					{
						param.Size = fields[0].Size;
					}
					this.dataFieldParameters.Add(param);
				}				
			}

			try
			{
				for (int i=0; i < properties.Length; i++)
				{
					KeyFieldAttribute[] fields = (KeyFieldAttribute[])properties[i].GetCustomAttributes(typeof(KeyFieldAttribute), true);
				
					if (fields.Length > 0)
					{
						object value = properties[i].GetValue(o, null);
						
						if (value == null)
						{
							throw new Exception("Primary key字段" + fields[0].ColumnName + "不能为空值！！！");
						}

						if (value.GetType().IsEnum)
						{
							value = Convert.ToInt32(value);
						}
	
						ParamItem item;
						this.GetParameterName(fields[0].ColumnName,out item);
						DALParameter param = new DALParameter(item.paramName, value);
					
						keyParams.Add(item);

						param.Type = fields[0].Type;
                    
						if (fields[0].Size != 0)
						{
							param.Size = fields[0].Size;
						}
						this.keyFieldParameters.Add(param);
					}				
				}
			}
			catch(Exception ex)
			{
				throw new Exception("3213213" + ex.Message);
			}

			string sql = "";
			if (action == "insert")
			{
				StringBuilder sb = new StringBuilder();
				sb.Append("insert into " + dataTable.TableName + "(");

				StringBuilder vb = new StringBuilder();
				vb.Append(" values(");

				foreach(ParamItem item in dataParams)
				{
					sb.Append(item.colName);
					sb.Append(", ");
					vb.Append(item.placeName);
					vb.Append(", ");
				}

				foreach(ParamItem item in keyParams)
				{
					sb.Append(item.colName);
					sb.Append(", ");
					vb.Append(item.placeName);
					vb.Append(", ");
				}
				sb.Remove(sb.Length - 2, 2);
				vb.Remove(vb.Length - 2, 2);
				sb.Append(")");
				vb.Append(")");
				sql = sb.ToString() + vb.ToString();
			}
			else
			{
				StringBuilder sb = new StringBuilder();
				sb.Append("update " + dataTable.TableName + " set ");

				StringBuilder vb = new StringBuilder();
				vb.Append(" where ");

				foreach(ParamItem item in dataParams)
				{
					sb.Append(item.colName);
					sb.Append("=");
					sb.Append(item.placeName);
					sb.Append(", ");
				}

				foreach(ParamItem item in keyParams)
				{
					vb.Append(item.colName);
					vb.Append("=");
					vb.Append(item.placeName);
					vb.Append(" and ");
				}
				sb.Remove(sb.Length - 2, 2);
				vb.Remove(vb.Length - 4, 4);
				sql = sb.ToString() + vb.ToString();
			}


			IDataCommand cmd = this._dataSource.GetTxtCommand(sql);
			//MessageBox.Show(sql);

			foreach(DALParameter p in this.dataFieldParameters)
			{
//				MessageBox.Show(p.Name+","+p.Type+","+p.Value.ToString());
				cmd.AddParameter(p.Name, p.Type, p.Size, p.Direction, p.Value);
			}

			foreach(DALParameter p in this.keyFieldParameters)
			{
				cmd.AddParameter(p.Name, p.Type, p.Size, p.Direction, p.Value);
			}
			
//			MessageBox.Show(sql);
			cmd.ExecuteNonQuery();
		}

		
		public void UpdateObjectStoredProcedure(object o, DataTableAttribute dataTable)
		{
			DALParameter keyParameter = null;
			PropertyInfo keyProperty  = null;
			PropertyInfo[] properties = o.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
			
			ClearParameters();
			
			for (int i=0; i < properties.Length; i++)
			{
				BaseFieldAttribute[] fields = (BaseFieldAttribute[])properties[i].GetCustomAttributes(typeof(BaseFieldAttribute), true);

				if (fields.Length > 0)
				{
					object value  = properties[i].GetValue(o, null);
					if (value.GetType().IsEnum)
					{
						value = Convert.ToInt32(value);
					}
					
					DALParameter param = new DALParameter("@" + fields[0].ColumnName, value);
					
					DataFieldAttribute dataField = fields[0] as DataFieldAttribute;
					if (dataField != null)
					{
						param.Type = dataField.Type;

						if (dataField.Size != 0)
						{
							param.Size = dataField.Size;
						}
					}
					else if (fields[0] is KeyFieldAttribute)
					{
						keyProperty  = properties[i];
						keyParameter = param;
						
						param.Direction = ParameterDirection.InputOutput;
					}

					AddParameter(param);
				}
			}				

			if (keyProperty == null || keyParameter == null)
			{
				throw new ArgumentException("The object " + o + " doesn't has a KeyField attribute");
			}


			//ExecSP_NonQuery(dataTable.UpdateStoredProcedure);
			IDataCommand cmd = this._dataSource.GetSpCommand(dataTable.UpdateStoredProcedure);
			cmd.ExecuteNonQuery();
			
			keyProperty.SetValue(o, Convert.ChangeType(keyParameter.Value, keyProperty.PropertyType), null);
		}
		
		
		
		public void UpdateObject(object o)
		{		
			Type type = o.GetType();
			DataTableAttribute[] dataTables = (DataTableAttribute[])type.GetCustomAttributes(typeof(DataTableAttribute), true);
			
			if (dataTables.Length > 0)
			{
				if (dataTables[0].UpdateStoredProcedure != "")
				{
					UpdateObjectStoredProcedure(o, dataTables[0]);
				}
				else
				{
					UpdateObjectSql(o, dataTables[0]);
				}	
			}
			else
			{
				throw new ArgumentException("The DataTable attribute wasn't found in the object [o parameter]");			
			}
		}
		

		public void UpdateObjects(IEnumerable enumObjects)
		{
			foreach(object o in enumObjects)
			{
				UpdateObject(o);
			}
		}
				
		
		public int RetrieveChildObjects(object foreignKeyValue, ArrayList objects, Type childType)
		{
			int result = 0;
		
			DataTableAttribute[] dataTables = (DataTableAttribute[])childType.GetCustomAttributes(typeof(DataTableAttribute), true);
			
			if (dataTables.Length > 0)
			{
			
				PropertyInfo[] properties = childType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
				string clause = "";

				for (int i=0; i < properties.Length; i++)
				{
					ForeignKeyFieldAttribute[] keys = (ForeignKeyFieldAttribute[])properties[i].GetCustomAttributes(typeof(ForeignKeyFieldAttribute), false);

					if (keys.Length > 0)
					{
						if (properties[i].PropertyType == typeof(string) || properties[i].PropertyType == typeof(char))
						{
							clause = "[" + keys[0].ColumnName + "] = '" + foreignKeyValue + "'";
						}
						else
						{
							clause = "[" + keys[0].ColumnName + "] = " + foreignKeyValue;
						}

						break;
					}
				}
				
				if (clause == "")
				{
					throw new ArgumentException("The object [" + childType + "] doesn't have a ForeignKey attribute");
				
				}


				StringBuilder sb = new StringBuilder("SELECT ");
				for (int i=0; i < properties.Length; i++)
				{
					BaseFieldAttribute[] fields = (BaseFieldAttribute[])properties[i].GetCustomAttributes(typeof(BaseFieldAttribute), true);

					if (fields.Length > 0)
					{
						sb.Append("[");
						sb.Append(fields[0].ColumnName);
						sb.Append("], ");
					}
				}
				
				// remove the last ','
				sb.Remove(sb.Length - 2, 2);				

				sb.Append(" FROM [");
				sb.Append(dataTables[0].TableName);
				sb.Append("] ");
				sb.Append("WHERE ");
				sb.Append(clause);
								
				
				IDataReader reader = null;
				try
				{
					//reader = ExecQuery_DataReader(sb.ToString());	
                    //IDataCommand cmd = this._dataSource.GetTxtCommand(sb.ToString());
                    reader = this._dataSource.ExecuteTxtReader(sb.ToString());// cmd.ExecuteReader();
					
					while (reader.Read())
					{
						result++;
						objects.Add(CreateFromReader(reader, childType));
					}
				}
				catch (Exception e)
				{
					throw new DALException("Failed to retrieve child objects [" + childType + "] with sql " + sb.ToString(), e);
				}
				finally
				{
					if (reader != null)
					{
						reader.Dispose();
					}
				}
			}
			else
			{
				throw new ArgumentException("The DataTable attribute wasn't found in the object [keyValue parameter]");
			}
			
			return result;
		}
		

		public void UpdateDataTable(DataTable dataTable)
		{
			if(dataTable == null) return;
			if(dataTable.Rows.Count<1) return;

			//DALQueryBuilder qBuilder = null;
			ArrayList dataParams = new ArrayList();
			ArrayList keyParams = new ArrayList();
			ArrayList keyCols = new ArrayList();
			ArrayList cols = new ArrayList();

			foreach(DataColumn col in dataTable.PrimaryKey)
			{
				keyCols.Add(col.ColumnName);
			}

			foreach(DataColumn col in dataTable.Columns)
			{
				cols.Add(col.ColumnName);
			}

			foreach(DataRow row in dataTable.Rows)
			{
				if(row.RowState == DataRowState.Unchanged)
				{
					continue;
				}

				this.keyFieldParameters.Clear();
				this.dataFieldParameters.Clear();
				dataParams.Clear();
				keyParams.Clear();

				foreach(string colname in cols)
				{
					object value = row[colname];
	
					if (value.GetType().IsEnum)
					{
						try
						{
							value = Convert.ToInt32(value);
						}
						catch
						{
							value = 0;
						}
					}

					if (value.GetType().IsEnum)
					{
						value = Convert.ToInt32(value);
					}

					ParamItem item;
					this.GetParameterName(colname,out item);
					item.Value = value;
					DALParameter param = new DALParameter(item.paramName, value);
			
					dataParams.Add(item);

					if(dataTable.Columns[colname].DataType.Name == "DateTime")
					{
						param.Type = "DateTime";
						param.Size = 0;
					}

					if (value.ToString().Length==0)
					{
						param.Size = 1;
						param.Value = "";
					}
                   
					this.dataFieldParameters.Add(param);
				}

				foreach(string colname in keyCols)
				{
					object value = row[colname];
					if(row.RowState == DataRowState.Modified)
					{
						value = row[colname, DataRowVersion.Original];
					}
	
					if (value == null || value.ToString().Length==0)
					{
						throw new Exception("关键字段" + colname + "有空值!");
					}

					if (value.GetType().IsEnum)
					{
						value = Convert.ToInt32(value);
					}

					ParamItem item;
					this.GetParameterName(colname,out item);
					if(item.placeName.Trim().Length>1)
					{
						item.placeName += "_w";
						item.paramName += "_w";
					}
					item.Value = value;
					DALParameter param = new DALParameter(item.paramName, value);
			
					keyParams.Add(item);

					if(dataTable.Columns[colname].DataType.Name == "DateTime")
					{
						param.Type = "DateTime";
						param.Size = 0;
					}
                   
					this.keyFieldParameters.Add(param);
				}


				string sql = "";
				if (row.RowState == DataRowState.Added)
				{
					StringBuilder sb = new StringBuilder();
					sb.Append("insert into " + dataTable.TableName + "(");

					StringBuilder vb = new StringBuilder();
					vb.Append(" values(");

					foreach(ParamItem item in dataParams)
					{
						sb.Append(item.colName);
						sb.Append(", ");
						vb.Append(item.placeName);
						vb.Append(", ");
					}

//					foreach(ParamItem item in keyParams)
//					{
//						sb.Append(item.colName);
//						sb.Append(", ");
//						vb.Append(item.placeName);
//						vb.Append(", ");
//					}
					sb.Remove(sb.Length - 2, 2);
					vb.Remove(vb.Length - 2, 2);
					sb.Append(")");
					vb.Append(")");
					sql = sb.ToString() + vb.ToString();
				}

				if(row.RowState == DataRowState.Modified)
				{
					StringBuilder sb = new StringBuilder();
					sb.Append("update " + dataTable.TableName + " set ");

					StringBuilder vb = new StringBuilder();
					vb.Append(" where ");

					StringBuilder tmpsb = new StringBuilder();
					tmpsb.Append("update " + dataTable.TableName + " set ");
					StringBuilder tmpvb = new StringBuilder();
					tmpvb.Append(" where ");

					foreach(ParamItem item in dataParams)
					{
						sb.Append(item.colName);
						sb.Append("=");
						sb.Append(item.placeName);
						sb.Append(", ");

						tmpsb.Append(item.colName);
						tmpsb.Append("=");
						tmpsb.Append(item.Value.ToString());
						tmpsb.Append(", ");
					}

					foreach(ParamItem item in keyParams)
					{
						vb.Append(item.colName);
						vb.Append("=");
						vb.Append(item.placeName);
						vb.Append(" and ");

						tmpvb.Append(item.colName);
						tmpvb.Append("=");
						tmpvb.Append(item.Value.ToString());
						tmpvb.Append(" and ");
					}
					sb.Remove(sb.Length - 2, 2);
					vb.Remove(vb.Length - 4, 4);
					sql = sb.ToString() + vb.ToString();
					//MessageBox.Show(tmpsb.ToString() + tmpvb.ToString());
				}
				
				if(sql.Length<1) continue;

//				MessageBox.Show(sql);
				IDataCommand cmd = this._dataSource.GetTxtCommand(sql);

				foreach(DALParameter p in this.dataFieldParameters)
				{
					cmd.AddParameter(p.Name, p.Type, p.Size, p.Direction, p.Value);
				}

				if(row.RowState == DataRowState.Modified)
				{
					foreach(DALParameter p in this.keyFieldParameters)
					{
						cmd.AddParameter(p.Name, p.Type, p.Size, p.Direction, p.Value);
					}
				}
			
				cmd.ExecuteNonQuery();
			}
		}


		public void DeleteDataTable(DataTable dataTable)
		{
			if(dataTable == null) return;
			if(dataTable.Rows.Count<1) return;

			//DALQueryBuilder qBuilder = null;
			ArrayList keyParams = new ArrayList();
			ArrayList keyCols = new ArrayList();

			foreach(DataColumn col in dataTable.PrimaryKey)
			{
				keyCols.Add(col.ColumnName);
			}

			foreach(DataRow row in dataTable.Rows)
			{
				this.keyFieldParameters.Clear();
				keyParams.Clear();

				foreach(string colname in keyCols)
				{
					object value = row[colname];
	
					if (value == null) continue;
					if (value.GetType().IsEnum)
					{
						value = Convert.ToInt32(value);
					}

					if (value.ToString().Length==0) continue;

					ParamItem item;
					this.GetParameterName(colname,out item);
					DALParameter param = new DALParameter(item.paramName, value);
			
					keyParams.Add(item);
					//param.Type = value.GetType().Name;
					//MessageBox.Show("param.Name=" + param.Name + ",param.Value=" + param.Value.ToString());

					this.keyFieldParameters.Add(param);
				}


				string sql = "";
		
				StringBuilder sb = new StringBuilder();
				sb.Append("delete from " + dataTable.TableName);

				StringBuilder vb = new StringBuilder();
				vb.Append(" where ");

				foreach(ParamItem item in keyParams)
				{
					vb.Append(item.colName);
					vb.Append("=");
					vb.Append(item.placeName);
					vb.Append(" and ");
				}
				vb.Remove(vb.Length - 4, 4);
				sql = sb.ToString() + vb.ToString();

				if(sql.Length<1) continue;

				//MessageBox.Show(sql);
				IDataCommand cmd = this._dataSource.GetTxtCommand(sql);

				//string s="";
				foreach(DALParameter p in this.keyFieldParameters)
				{
					//MessageBox.Show("p.Name=" + p.Name + ",p.Value=" + p.Value.ToString() + ",p.Type=" + p.Type + ",p.Direction=" + p.Direction.ToString());
					cmd.AddParameter(p.Name, p.Type, p.Size, p.Direction, p.Value);
					//s += "," + p.Name + "=" + p.Value;
				}
				//MessageBox.Show(s);
			
				cmd.ExecuteNonQuery();
			}
		}
	}	
}