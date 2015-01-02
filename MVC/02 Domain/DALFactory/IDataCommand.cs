using System;
using System.Data;
using System.Collections;

namespace BLS_AspNet.DALFactory
{
	/// <summary>
	/// Summary description for IDataCommand.
	/// </summary>
	public interface IDataCommand : ICloneable
	{
		void SetParameterValue(string paramName, object paramValue);
		object GetParameterValue(string paramName); 

		//this is for input Int params for example
		void AddParameter(
			string paramName, string paramType, int paramSize,
			ParameterDirection paramDirection, object paramValue);
		void AddParameter(
			string paramName, Enum paramType, 
			ParameterDirection paramDirection, object paramValue);
		//this is for input string params for example
		void AddParameter(
			string paramName, Enum paramType, int paramSize,
			ParameterDirection paramDirection, object paramValue);
		//this is for output int params for example 
		void AddParameter(
			string paramName, Enum paramType, ParameterDirection paramDirection);
		//this is for output string params for example 
		void AddParameter(
			string paramName, Enum paramType, int paramSize, ParameterDirection paramDirection);

		int ExecuteNonQuery();
		//int ExecuteNonQuery(string sql);
		int ExecuteNonQuery(IDbConnection con, IDbTransaction tran);

		IDataReader ExecuteReader();
		DataSet ExecuteDataset();
		DataTable ExecuteDataTable();
		object ExecuteScalar();
		IDbDataParameter CreateParameter();
		void AddParameter(IDbDataParameter param);

		string Name { get; set; }
		IDataSource DataSource { get; }

		IDbCommand DbCommand { get; }

		IList Parameters { get; }
	}
}
