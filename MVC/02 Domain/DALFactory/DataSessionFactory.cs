using System;
using System.Runtime.Remoting.Messaging;

namespace BLS_AspNet.DALFactory
{
	/// <summary>
	/// DataSessionFactory retrieves/creates the current DataSession.
	/// </summary>
	public class DataSessionFactory
	{
		private const string TLS_SLOT = "DAC2_DataSession";

		//only static methods
		private DataSessionFactory() {}

		public static IDataSession GetCurrentSession() 
		{
			IDataSession dataSession = CallContext.GetData(TLS_SLOT) as IDataSession;
			if(dataSession == null)
			{
				dataSession = new DataSession();
				CallContext.SetData(TLS_SLOT, dataSession);
			}

			return dataSession;
		}
	}
}
