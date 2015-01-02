using System;
using System.Data;
using System.Data.Common;
using System.Collections;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using System.Runtime.Remoting.Messaging;

namespace BLS_AspNet.DALFactory
{
    /// <summary>
    /// DataSource is a facade class and represents a certain database.
    /// Produces connections and commands.
    /// Uses DataCommandFactory internally.
    /// </summary>
    class DataSource : IDataSource
    {
        private string _name = "";
        private DataProvider _provider = null;
        private string _connectionString = "";
        private DataCommandFactory _commandFactory = null;
        private IDbConnection _templateConnection = null;
        private IDbCommand _templateCommand = null;
        private DbDataAdapter _templateDataAdapter = null;

        public DataSource(
            string name, DataProvider provider, string connectionString,
            string commandDir, string commandFileMask)
        {
            _name = name;
            _provider = provider;
            _connectionString = connectionString;
            _commandFactory = new DataCommandFactory(this, commandDir, commandFileMask);
            _templateConnection = (IDbConnection)Activator.CreateInstance(_provider.ConnectionObjectType);
            _templateCommand = (IDbCommand)Activator.CreateInstance(_provider.CommandObjectType);
            _templateDataAdapter = (DbDataAdapter)Activator.CreateInstance(_provider.DataAdapterObjectType);
        }

        public string Name { get { return _name; } }

        public string ConnString
        {
            get
            {
                return this._connectionString;
            }
            set
            {
                this._connectionString = value;
            }
        }

        public DataProvider Provider
        {
            get
            {
                return _provider;
            }
        }

        public string conn
        {
            get
            {
                return _templateConnection.GetType().Name;
            }
        }

        public IDbConnection CreateConnection()
        {
            //IDbConnection dbCon = (IDbConnection)Activator.CreateInstance(_provider.ConnectionObjectType);
            IDbConnection dbCon = (IDbConnection)((ICloneable)_templateConnection).Clone();
            dbCon.ConnectionString = _connectionString;
            return dbCon;
        }

        public IDataCommand GetCommand(string commandName)
        {
            return _commandFactory.GetCommand(commandName);
        }

        public IDataCommand CreateCommand(string commandName, string commandText, CommandType commandType)
        {
            //TODO: decide whether to delegate or do it here

            //IDbCommand dbCmd = (IDbCommand)Activator.CreateInstance(_provider.CommandObjectType);
            IDbCommand dbCmd = (IDbCommand)((ICloneable)_templateCommand).Clone();
            dbCmd.CommandText = commandText;
            dbCmd.CommandType = commandType;

            DataCommand cmd = new DataCommand(commandName, dbCmd, this);

            return cmd;
        }

        private DataCommand CreateCommand(CommandType commandType, string commandText)
        {
            //TODO: decide whether to delegate or do it here

            //IDbCommand dbCmd = (IDbCommand)Activator.CreateInstance(_provider.CommandObjectType);
            IDbCommand dbCmd = (IDbCommand)((ICloneable)_templateCommand).Clone();
            dbCmd.CommandText = commandText;
            dbCmd.CommandType = commandType;

            DataCommand cmd = new DataCommand("", dbCmd, this);

            return cmd;
        }

        public IDataCommand GetTxtCommand(string commandText)
        {
            DataCommand cmd = CreateCommand(CommandType.Text, commandText);
            return cmd;
        }

        public IDataCommand GetSpCommand(string commandText)
        {
            DataCommand cmd = CreateCommand(CommandType.StoredProcedure, commandText);
            return cmd;
        }

        public DbDataAdapter CreateAdapter()
        {
            DbDataAdapter adapter = (DbDataAdapter)((ICloneable)_templateDataAdapter).Clone();
            return adapter;
        }

        public IDALEngine CreateDALEngine()
        {
            DALEngine dal = new DALEngine(this);
            return dal;
        }

        public int ExecuteTxtNonQuery(string sql)
        {
            IDataCommand cmd = this.GetTxtCommand(sql);
            return cmd.ExecuteNonQuery();
        }

        public IDataReader ExecuteTxtReader(string sql)
        {
            IDataCommand cmd = this.GetTxtCommand(sql);
            return cmd.ExecuteReader();
        }

        public DataSet ExecuteTxtDataset(string sql)
        {
            IDataCommand cmd = this.GetTxtCommand(sql);
            return cmd.ExecuteDataset();
        }

        public DataTable ExecuteTxtDataTable(string sql)
        {
            IDataCommand cmd = this.GetTxtCommand(sql);
            return cmd.ExecuteDataTable();
        }

        public object ExecuteTxtScalar(string sql)
        {
            IDataCommand cmd = this.GetTxtCommand(sql);
            return cmd.ExecuteScalar();
        }

        public int ExecuteSpNonQuery(string sql)
        {
            IDataCommand cmd = this.GetSpCommand(sql);
            return cmd.ExecuteNonQuery();
        }

        public IDataReader ExecuteSpReader(string sql)
        {
            IDataCommand cmd = this.GetSpCommand(sql);
            return cmd.ExecuteReader();
        }

        public DataSet ExecuteSpDataset(string sql)
        {
            IDataCommand cmd = this.GetSpCommand(sql);
            return cmd.ExecuteDataset();
        }

        public DataTable ExecuteSpDataTable(string sql)
        {
            IDataCommand cmd = this.GetSpCommand(sql);
            return cmd.ExecuteDataTable();
        }

        public object ExecuteSpScalar(string sql)
        {
            IDataCommand cmd = this.GetSpCommand(sql);
            return cmd.ExecuteScalar();
        }

        public object RetrieveObject(object keyValue, Type objType)
        {
            IDALEngine dal = this.CreateDALEngine();
            return dal.RetrieveObject(keyValue, objType);
        }

        public void RetrieveObject(object obj)
        {
            IDALEngine dal = this.CreateDALEngine();
            dal.RetrieveObject(obj);
        }

        public void UpdateObject(object o)
        {
            IDALEngine dal = this.CreateDALEngine();
            dal.UpdateObject(o);
        }

        public void CreateFromDataRow(DataRow reader, object obj)
        {
            IDALEngine dal = this.CreateDALEngine();
            dal.CreateFromDataRow(reader, obj);
        }

        public void CreateFromReader(IDataReader reader, object obj)
        {
            IDALEngine dal = this.CreateDALEngine();
            dal.CreateFromReader(reader, obj);
        }

        public void UpdateDataTable(DataTable table)
        {
            IDALEngine dal = this.CreateDALEngine();
            dal.UpdateDataTable(table);
        }

        public void DeleteDataTable(DataTable dataTable)
        {
            IDALEngine dal = this.CreateDALEngine();
            dal.DeleteDataTable(dataTable);
        }

    }
}
