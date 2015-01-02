///////////////////////////////////////////////////////////////////////////
// Description: Data Access class for the table 'DacHelper'
// lihg: 2008年5月26日, 18:01:36
// 
///////////////////////////////////////////////////////////////////////////
using System;
using System.Data;
using System.Data.Common;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Data.OracleClient;
using System.Collections;
using System.Reflection;
using System.Text;


namespace BLS_AspNet.DALFactory
{
    /// <summary>
    /// 目的: 帮助存取数据.
    /// </summary>
    public class DacHelper
    {
        private DataRow _row = null;
        public IDbConnection conn;
        private DataTable _table;
        public string tablename;

        public SqlDataAdapter sqladapter;
        public OleDbDataAdapter oledbadapter;
        public OracleDataAdapter oracleadapter;
        public OdbcDataAdapter odbcadapter;

        public SqlCommandBuilder sqlbuilder;
        public OleDbCommandBuilder oledbbuilder;
        public OracleCommandBuilder oraclebuilder;
        public OdbcCommandBuilder odbcbuilder;

        public bool addnew = false;
        private bool _hasRetrieved = false;
        private DataSet dataset = null;
        public IDataSource _ds;
        public string connstring;

        public DacHelper(string dsname)
        {
            _ds = DataSourceFactory.GetDataSource(dsname);
            connstring = _ds.ConnString;
            conn = _ds.CreateConnection();
        }

        public DacHelper()
        {
            _ds = DataSourceFactory.GetDataSource();
            connstring = _ds.ConnString;
            conn = _ds.CreateConnection();
        }

        public IDataSource DataSource
        {
            set
            {
                _ds = value;
                conn = _ds.CreateConnection();
                connstring = _ds.ConnString;
            }
        }

        public DataRow row
        {
            get
            {
                return _row;
            }
            set
            {
                _row = value;
            }
        }

        public DataSet ds
        {
            get
            {
                return dataset;
            }
            set
            {
                dataset = value;
            }
        }

        public DataTable table
        {
            set
            {
                _table = value;
            }
            get
            {
                return _table;
            }
        }

        public bool hasRetrieved
        {
            get
            {
                return this._hasRetrieved;
            }
            set
            {
                this._hasRetrieved = value;
            }
        }

        public void RetrieveData()
        {
            this.RetrieveData(this.BuildQuery(this));
        }

        public void RetrieveEmpty()
        {
            this.RetrieveData("select * from " + tablename + " where 1=0");
        }

        public void RetrieveData(string sql)
        {
            try
            {
                switch (_ds.conn)
                {
                    case "SqlConnection":
                        this.RetrieveSqlData(sql);
                        break;
                    case "OdbcConnection":
                        this.RetrieveOdbcData(sql);
                        break;
                    case "OleDbConnection":
                        this.RetrieveOleDbData(sql);
                        break;
                    case "OracleConnection":
                        this.RetrieveOracleData(sql);
                        break;
                    default:
                        break;
                }

                table = ds.Tables[0];
                if (table.Rows.Count > 0)
                {
                    this.row = table.Rows[0];
                    addnew = false;
                }
                else
                {
                    this.row = table.NewRow();
                    addnew = true;
                }
                this.hasRetrieved = true;
            }
            catch (Exception ex)
            {
                throw new Exception("提取数据错误：RetrieveData(" + sql + ")" + ex.Message);
            }
        }

        public void UpdateData()
        {
            try
            {
                if (addnew) table.Rows.Add(row);
                switch (this._ds.conn)
                {
                    case "SqlConnection":
                        this.sqladapter.Update(this.table);
                        break;
                    case "OdbcConnection":
                        this.odbcadapter.Update(this.table);
                        break;
                    case "OleDbConnection":
                        this.oledbadapter.Update(this.table);
                        break;
                    case "OracleConnection":
                        this.oracleadapter.Update(this.table);
                        break;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("数据保存出错：UpdateData()" + ex.Message);
            }
        }

        public void RetrieveOleDbData(string sql)
        {
            this.oledbadapter = new OleDbDataAdapter(sql, (OleDbConnection)this.conn);
            this.oledbbuilder = new OleDbCommandBuilder(this.oledbadapter);
            ds = new DataSet();
            this.oledbadapter.Fill(ds, tablename);
        }

        public void RetrieveOdbcData(string sql)
        {
            this.odbcadapter = new OdbcDataAdapter(sql, (OdbcConnection)this.conn);
            this.odbcbuilder = new OdbcCommandBuilder(this.odbcadapter);
            ds = new DataSet();
            this.odbcadapter.Fill(ds, tablename);
        }

        public void RetrieveOracleData(string sql)
        {
            this.oracleadapter = new OracleDataAdapter(sql, (OracleConnection)this.conn);
            this.oraclebuilder = new OracleCommandBuilder(this.oracleadapter);
            ds = new DataSet();
            this.oracleadapter.Fill(ds, tablename);
        }

        public void RetrieveSqlData(string sql)
        {
            this.sqladapter = new SqlDataAdapter(sql, (SqlConnection)this.conn);
            this.sqlbuilder = new SqlCommandBuilder(this.sqladapter);
            ds = new DataSet();
            this.sqladapter.Fill(ds, tablename);
        }


        //构造select语句
        public string BuildQuery(object o)
        {
            Type objType = o.GetType();

            DataTableAttribute[] dataTables = (DataTableAttribute[])objType.GetCustomAttributes(typeof(DataTableAttribute), true);

            if (dataTables.Length > 0)
            {

                PropertyInfo[] properties = objType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
                StringBuilder cb = new StringBuilder();
                string clause = "";
                int k = 0;

                for (int i = 0; i < properties.Length; i++)
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
                            if (value != "0")
                            {
                                cb.Append(value + " and ");
                            }
                            else
                            {
                                throw new Exception("Primary key字段" + keys[0].ColumnName + "不能为空值！！！");
                            }
                        }
                    }
                }
                if (k == 0) throw new Exception("数据库表" + dataTables[0].TableName + "没有 Primary key字段！！！");
                cb.Remove(cb.Length - 4, 4);
                clause = cb.ToString();


                StringBuilder sb = new StringBuilder("SELECT ");
                for (int i = 0; i < properties.Length; i++)
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

                return sb.ToString();

            }
            else
            {
                throw new ArgumentException("The DataTable attribute wasn't found in the object [keyValue parameter]");
            }
        }

        /// <summary>
        /// 赋sql语句参数值
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="paras"></param>
        public static void PrepareCommand(IDataCommand cmd, Hashtable paras)
        {
            IDbDataParameter para = null;
            if (paras != null)
            {
                foreach (DictionaryEntry de in paras)
                {
                    para = cmd.CreateParameter();
                    para.ParameterName = de.Key.ToString();
                    para.Value = de.Value.ToString();
                    cmd.AddParameter(para);
                }
            }
        }
    }
}
