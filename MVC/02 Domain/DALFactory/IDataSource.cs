using System;
using System.Data;
using System.Data.Common;

namespace BLS_AspNet.DALFactory
{
	/// <summary>
	/// Summary description for IDataSource.
	/// </summary>
	public interface IDataSource
	{
        IDataCommand GetCommand(string commandName);
		string Name { get; }
		string conn { get; }
		string ConnString{ get;set;}

		IDbConnection CreateConnection();
		IDataCommand CreateCommand(string commandName, string commandText, CommandType commandType);
		IDataCommand GetTxtCommand(string commandText);
		IDataCommand GetSpCommand(string commandText);
		DbDataAdapter CreateAdapter();
		IDALEngine CreateDALEngine();

		int ExecuteTxtNonQuery(string sql);
		IDataReader ExecuteTxtReader(string sql);
		DataSet ExecuteTxtDataset(string sql);
		DataTable ExecuteTxtDataTable(string sql);
		object ExecuteTxtScalar(string sql);

		int ExecuteSpNonQuery(string spname);
		IDataReader ExecuteSpReader(string spname);
		DataSet ExecuteSpDataset(string spname);
		DataTable ExecuteSpDataTable(string spname);
		object ExecuteSpScalar(string spname);

		object RetrieveObject(object keyValue, Type objType);
		void RetrieveObject(object obj);
		void UpdateObject(object o);
		void CreateFromDataRow(DataRow reader, object instance);
		void CreateFromReader(IDataReader reader, object instance);
		void UpdateDataTable(DataTable table);
		void DeleteDataTable(DataTable dataTable);
	}
}
