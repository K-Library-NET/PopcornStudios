using System;
using System.Data;

namespace BLS_AspNet.DALFactory
{

	[AttributeUsage(AttributeTargets.Property)]
	public class BaseFieldAttribute : Attribute
	{
		string columnName;
		string dbType = "VarChar";
		int    size   = 0;
				
		public string Type
		{
			get { return dbType;  }
			set { dbType = value; }
		}
		
		public int Size 
		{
			get { return size;  }
			set { size = value; }
		}	
		public BaseFieldAttribute(string columnName)
		{
			this.columnName = columnName;
		}
		
		public string ColumnName
		{
			get { return columnName;  }
			set { columnName = value; }
		}	
	}


	[AttributeUsage(AttributeTargets.Property)]
	public class DataFieldAttribute : BaseFieldAttribute
	{
		public DataFieldAttribute(string columnName) : base(columnName)
		{
			
		}
	};
	
	[AttributeUsage(AttributeTargets.Property)]
	public class KeyFieldAttribute : BaseFieldAttribute
	{
		public KeyFieldAttribute(string columnName) : base(columnName)
		{
		
		}
	};
	
	[AttributeUsage(AttributeTargets.Property)]
	public class ForeignKeyFieldAttribute : BaseFieldAttribute
	{
		public ForeignKeyFieldAttribute(string columnName) : base(columnName)
		{			
			
		}
		
	};	
	
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
	public class DataTableAttribute : Attribute
	{
		string tableName;
		string updateStoredProcedure   = "";
		
		public DataTableAttribute(string tableName)
		{
			this.tableName = tableName;
		}
		
		
		public string TableName
		{
			get { return tableName;  }
			set { tableName = value; }
		}		
		
		
		public string UpdateStoredProcedure
		{
			get { return updateStoredProcedure;  }
			set { updateStoredProcedure = value; }
		}
	}
}