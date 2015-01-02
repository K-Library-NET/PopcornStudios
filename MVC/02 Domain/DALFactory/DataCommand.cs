using System;
using System.Data;
using System.Data.Common;
using System.Collections;
//using System.Windows.Forms;

namespace BLS_AspNet.DALFactory
{
	/// <summary>
	/// DataCommand wraps one (or more) IDbCommands.
	/// </summary>
	class DataCommand : IDataCommand
	{
		private IDbCommand _dbCommand = null;
		private string _commandName = "";
		private DataSource _dataSource = null;

		public DataCommand(string commandName, IDbCommand dbCommand, DataSource ds)
		{
			_commandName = commandName;
			_dbCommand = dbCommand;
			_dataSource = ds;
		}

		public string Name 
		{ 
			get { return _commandName; }
			set { _commandName = value; } 
		}
		public IDataSource DataSource { get { return _dataSource; } }

		public void SetParameterValue(string paramName, object paramValue) 
		{
			((IDbDataParameter)_dbCommand.Parameters[paramName]).Value = paramValue;
		}
		public object GetParameterValue(string paramName)
		{
			return ((IDbDataParameter)_dbCommand.Parameters[paramName]).Value;
		}

		public void AddParameter(
			string paramName, Enum paramType, 
			ParameterDirection paramDirection, object paramValue)
		{
			IDbDataParameter param = _dbCommand.CreateParameter();
			param.ParameterName = paramName;
			if(paramType is DbType) 
			{
				param.DbType = (DbType)paramType;
			}
			else 
			{
				if(paramType.GetType() != _dataSource.Provider.ParameterDbType) 
				{
					throw new DataSourceException("Invalid parameter type specified.");
				}

				_dataSource.Provider.ParameterDbTypeProperty.SetValue(param, paramType, null);
			}
			param.Direction = paramDirection;
			param.Value = paramValue;
			_dbCommand.Parameters.Add(param);
		}

		public void AddParameter(string paramName, string paramType,
								int paramSize, ParameterDirection paramDirection,
								object paramValue)
		{
			string pType = paramType;
			int pSize = paramSize;
			if(paramType == "DateTime")
				pSize = 0;

			if(this._dataSource.conn == "OleDbConnection" && paramType == "DateTime")
			{
				pType = "VarChar";
			}

			Enum dataType = (Enum)Enum.Parse(this._dataSource.Provider.ParameterDbType, pType, true);
			AddParameter(paramName, dataType, pSize, paramDirection, paramValue);
		}

		public void AddParameter(
			string paramName, Enum paramType, int paramSize,
			ParameterDirection paramDirection, object paramValue)
		{
			IDbDataParameter param = _dbCommand.CreateParameter();
			param.ParameterName = paramName;
			if(paramType is DbType) 
			{
				param.DbType = (DbType)paramType;
			}
			else 
			{
				if(paramType.GetType() != _dataSource.Provider.ParameterDbType) 
				{
					throw new DataSourceException("Invalid parameter type specified.");
				}

//				if(_dataSource.Provider.ParameterDbTypeProperty == null)
//				{
//					MessageBox.Show("_dataSource.Provider.ParameterDbTypeProperty is null");
//					throw new Exception("_dataSource.Provider.ParameterDbTypeProperty is null");
//				}

				_dataSource.Provider.ParameterDbTypeProperty.SetValue(param, paramType, null);
			}
			if (paramSize > 0)
			{
				param.Size = paramSize;
			}
			param.Direction = paramDirection;
			param.Value = paramValue;
			_dbCommand.Parameters.Add(param);
		}

		public 	void AddParameter(
			string paramName, Enum paramType, ParameterDirection paramDirection) 
		{
			IDbDataParameter param = _dbCommand.CreateParameter();
			param.ParameterName = paramName;
			if(paramType is DbType) 
			{
				param.DbType = (DbType)paramType;
			}
			else 
			{
				if(paramType.GetType() != _dataSource.Provider.ParameterDbType) 
				{
					throw new DataSourceException("Invalid parameter type specified.");
				}

				_dataSource.Provider.ParameterDbTypeProperty.SetValue(param, paramType, null);
			}
			param.Direction = paramDirection;
			_dbCommand.Parameters.Add(param);
		}

		public 	void AddParameter(
			string paramName, Enum paramType, int paramSize, ParameterDirection paramDirection) 
		{
			IDbDataParameter param = _dbCommand.CreateParameter();
			param.ParameterName = paramName;
			if(paramType is DbType) 
			{
				param.DbType = (DbType)paramType;
			}
			else 
			{
				if(paramType.GetType() != _dataSource.Provider.ParameterDbType) 
				{
					throw new DataSourceException("Invalid parameter type specified.");
				}

				_dataSource.Provider.ParameterDbTypeProperty.SetValue(param, paramType, null);
			}
			param.Size = paramSize;
			param.Direction = paramDirection;
			_dbCommand.Parameters.Add(param);
		}

		public object Clone() 
		{
			return new DataCommand(
				_commandName, (IDbCommand)((ICloneable)_dbCommand).Clone(), _dataSource);
		}

		public int ExecuteNonQuery() 
		{
			int recordsAffected = 0;
			IDataSession s = DataSessionFactory.GetCurrentSession();
			IDbConnection con = null;
			IDbTransaction tran = null;

			if(s.IsInTransaction && s.Execution == CommandExecution.NoOutputQueued) 
			{
				s.QueueCommand(this);
			}
			else
			{

				if(s.IsInTransaction) 
				{
					con = s.GetConnection(_dataSource);
					tran = s.GetTransaction(con);
				}
				else 
				{
					con = _dataSource.CreateConnection();
					con.Open();
				}

				_dbCommand.Connection = con;
				_dbCommand.Transaction = tran;

				recordsAffected = _dbCommand.ExecuteNonQuery();

				if(!s.IsInTransaction) 
				{
					con.Close();
				}
			}

			return recordsAffected;
		}

		//public int ExecuteNonQuery(string sql)
		//{

		//}

		public int ExecuteNonQuery(IDbConnection con, IDbTransaction tran) 
		{
			_dbCommand.Connection = con;
			_dbCommand.Transaction = tran;

			return _dbCommand.ExecuteNonQuery();
		}

		public IDataReader ExecuteReader() 
		{
			IDataSession s = DataSessionFactory.GetCurrentSession();
			IDbConnection con = null;
			IDbTransaction tran = null;

			if(!s.IsInTransaction) 
			{
				con = _dataSource.CreateConnection();
				con.Open();
			}
			else 
			{
				con = s.GetConnection(_dataSource);
				tran = s.GetTransaction(con);
			}

			_dbCommand.Connection = con;
			_dbCommand.Transaction = tran;

			CommandBehavior commandBehaviour = CommandBehavior.Default;
			if(!s.IsInTransaction) 
			{
				commandBehaviour = CommandBehavior.CloseConnection;
			}

			IDataReader dr = _dbCommand.ExecuteReader(commandBehaviour);

			return dr;
		}

		public IDbCommand DbCommand 
		{
			get { return _dbCommand; }
			//set { _dbCommand = value; }
		}

		public IList Parameters
		{ 
			get { return _dbCommand.Parameters; } 
		}

		public DataSet ExecuteDataset()
		{
			IDataSession s = DataSessionFactory.GetCurrentSession();
			IDbConnection con = null;
			IDbTransaction tran = null;

			if(!s.IsInTransaction) 
			{
				con = _dataSource.CreateConnection();
				con.Open();
			}
			else 
			{
				con = s.GetConnection(_dataSource);
				tran = s.GetTransaction(con);
			}

			_dbCommand.Connection = con;
			_dbCommand.Transaction = tran;

			IDbDataAdapter da = (IDbDataAdapter)this._dataSource.CreateAdapter();
			da.SelectCommand = this._dbCommand;
			DataSet ds = new DataSet();

			//fill the DataSet using default values for DataTable names, etc.
			da.Fill(ds);

			if(!s.IsInTransaction) 
			{
				con.Close();
			}
			
			return ds;		
		}

		public DataTable ExecuteDataTable()
		{
			return this.ExecuteDataset().Tables[0];
/*
			IDataSession s = DataSessionFactory.GetCurrentSession();
			IDbConnection con = null;
			IDbTransaction tran = null;

			if(!s.IsInTransaction) 
			{
				con = _dataSource.CreateConnection();
				con.Open();
			}
			else 
			{
				con = s.GetConnection(_dataSource);
				tran = s.GetTransaction(con);
			}

			_dbCommand.Connection = con;
			_dbCommand.Transaction = tran;

			IDbDataAdapter da = (IDbDataAdapter)this._dataSource.CreateAdapter();
			//da.SelectCommand = this._dbCommand;
			//DataSet ds = new DataSet();
			DataTable dt = new DataTable();
		
			switch(da.GetType().Name)
			{
				case "OracleDataAdapter":
					System.Data.OracleClient.OracleDataAdapter oraDa = new System.Data.OracleClient.OracleDataAdapter();
					oraDa.SelectCommand = (System.Data.OracleClient.OracleCommand)this._dbCommand;
					oraDa.Fill(dt);
					break;
				case "OleDbDataAdapter":
					System.Data.OleDb.OleDbDataAdapter oleDa = new System.Data.OleDb.OleDbDataAdapter();
					oleDa.SelectCommand = (System.Data.OleDb.OleDbCommand)this._dbCommand;
					oleDa.Fill(dt);
					break;
				case "OdbcDataAdapter":
					System.Data.Odbc.OdbcDataAdapter odbcDa = new System.Data.Odbc.OdbcDataAdapter();
					odbcDa.SelectCommand = (System.Data.Odbc.OdbcCommand)this._dbCommand;
					odbcDa.Fill(dt);
					break;
				case "SqlDataAdapter":
					System.Data.SqlClient.SqlDataAdapter sqlDa = new System.Data.SqlClient.SqlDataAdapter();
					sqlDa.SelectCommand = (System.Data.SqlClient.SqlCommand)this._dbCommand;
					sqlDa.Fill(dt);
					break;
				default:
					break;
			}
		
			return dt;
			*/
		}

		public object ExecuteScalar()
		{
			IDataSession s = DataSessionFactory.GetCurrentSession();
			IDbConnection con = null;
			IDbTransaction tran = null;

			if(!s.IsInTransaction) 
			{
				con = _dataSource.CreateConnection();
				con.Open();
			}
			else 
			{
				con = s.GetConnection(_dataSource);
				tran = s.GetTransaction(con);
			}

			_dbCommand.Connection = con;
			_dbCommand.Transaction = tran;

			object retval = this._dbCommand.ExecuteScalar();
			
			if(!s.IsInTransaction) 
			{
				con.Close();
			}

			return retval;
		}

		public IDbDataParameter CreateParameter()
		{
			return _dbCommand.CreateParameter();
		}

		public void AddParameter(IDbDataParameter param)
		{
			_dbCommand.Parameters.Add(param);
		}

	}
}
