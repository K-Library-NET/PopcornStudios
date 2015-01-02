using System;
using System.Data;

namespace BLS_AspNet.DALFactory
{
	/// <summary>
	/// Summary description for IDataSession.
	/// </summary>
	public interface IDataSession
	{
		void BeginTransaction();
		void BeginTransaction(IsolationLevel isoLevel);
		void BeginTransaction(CommandExecution execution);
		void BeginTransaction(IsolationLevel isoLevel, CommandExecution execution);
		int CommitTransaction();
		void RollbackTransaction();
		int FlushTransaction();

		bool IsInTransaction { get; }
		IsolationLevel Isolation { get; }
		CommandExecution Execution { get; }

		void QueueCommand(IDataCommand cmd);

		IDbConnection GetConnection(IDataSource ds);
		IDbTransaction GetTransaction(IDbConnection con);
	}
}
