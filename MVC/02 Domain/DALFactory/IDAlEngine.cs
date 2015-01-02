using System;
using System.Data;
using System.Reflection;
using System.Collections;

namespace BLS_AspNet.DALFactory
{

	public interface IDALEngine : IDisposable
	{		
		DALParameter GetParameter(string name);
		
		void UpdateOutputParameters(IDbCommand cmd);
		
		void AddParameter(DALParameter param);

		//
		// Business objects methods
		//
		object CreateFromReader(IDataReader reader, Type objType);

		void CreateFromReader(IDataReader reader, object instance);

		void CreateFromDataRow(DataRow reader, object instance);
		
		object RetrieveObject(object keyValue, Type objType);

		void RetrieveObject(object o);
		
		void UpdateObject(object o);
		
		void UpdateObjectSql(object o, DataTableAttribute dataTable);
		
		void UpdateObjectStoredProcedure(object o, DataTableAttribute dataTable);

		void UpdateObjects(IEnumerable enumObjects);

		void UpdateDataTable(DataTable dataTable);

		void DeleteDataTable(DataTable dataTable);
		
		int RetrieveChildObjects(object foreignKeyValue, ArrayList objects, Type childType);
	}	
}