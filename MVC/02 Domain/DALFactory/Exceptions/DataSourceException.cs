using System;

namespace BLS_AspNet.DALFactory
{
	/// <summary>
	/// Summary description for DataSourceException.
	/// </summary>
	public class DataSourceException : ApplicationException
	{
		public DataSourceException() {}

		public DataSourceException(string message) : base(message) {}
	}
}
