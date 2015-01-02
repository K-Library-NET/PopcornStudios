using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;

namespace BLS_AspNet.DALFactory
{
	
	public class DALParameter
	{
		string name = "";
		object value;
		int size = 0;
		ParameterDirection direction = ParameterDirection.Input;
		string type = "VarChar";
		
		
		public DALParameter(string pName, object pValue)
		{
			name  = pName;
			value = pValue;
		}
		
		public DALParameter(string pName, string pType, object pValue)
		{
			name  = pName;
			value = pValue;
			type  = pType;
		}
		
		public DALParameter(string pName, string pType, ParameterDirection pDirection, object pValue)
		{
			name      = pName;
			value     = pValue;
			type      = pType;
			direction = pDirection;
			
		}
		
		public DALParameter(string pName, string pType, int pSize, object pValue)
		{
			name  = pName;
			value = pValue;
			type  = pType;
			size  = pSize;
		}
		
		public DALParameter(string pName, string pType, int pSize, ParameterDirection pDirection, object pValue)
		{
			name      = pName;
			value     = pValue;
			type      = pType;
			size      = pSize;
			direction = pDirection;
		}
		
		public string Name
		{
			get { return name;  }
			set { name = value; }
		}
		
		public object Value
		{
			get { return this.value;  }
			set { this.value = value; }
		}
		
		public int Size
		{
			get { return size;  }
			set { size = value; }
		}		
		
		public ParameterDirection Direction
		{
			get { return direction;  }
			set { direction = value; }
		}	
	
		public string Type
		{
			get { return type;  }
			set { type = value; }
		}	
		
	}





}