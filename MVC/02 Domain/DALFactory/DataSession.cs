using System;
using System.Data;
using System.Collections;
using System.Diagnostics;
using System.Threading;

namespace BLS_AspNet.DALFactory
{
	/// <summary>
	/// DataSession handles internally a connection/transaction.
	/// Thread-specific(serial access) i.e. thread-safe.
	/// </summary>
	class DataSession : IDataSession
	{
		private Hashtable _dbConnectionsByDataSource = new Hashtable();
		private Hashtable _dbTransactionsByDbConnection = new Hashtable();
		//private IDbConnection _con = null;
		//private IDbTransaction _tran = null;

		private bool _isInTransaction = false;
		private ArrayList _queuedCommands = new ArrayList();
		private CommandExecution _execution = CommandExecution.Immediate;
		private IsolationLevel _isolationLevel = IsolationLevel.ReadCommitted;

		public DataSession() {}

		public IList QueuedCommands { get { return _queuedCommands; } }
		//public IDbConnection Connection { get { return _con; } }

		public void QueueCommand(IDataCommand cmd) 
		{
			_queuedCommands.Add(cmd);
		}

		public IDbConnection GetConnection(IDataSource ds) 
		{
			if(!_isInTransaction) return null;

			IDbConnection con = _dbConnectionsByDataSource[ds] as IDbConnection;
			if(con == null) 
			{
				con = ds.CreateConnection();
				_dbConnectionsByDataSource.Add(ds, con);
				con.Open();
			}
			return con;
		}
		public IDbTransaction GetTransaction(IDbConnection con) 
		{
			if(!_isInTransaction) return null;

			IDbTransaction tran = _dbTransactionsByDbConnection[con] as IDbTransaction;
			if(tran == null) 
			{
				tran = con.BeginTransaction(_isolationLevel);
				_dbTransactionsByDbConnection.Add(con, tran);
			}
			return tran;
		}

		public void BeginTransaction() 
		{
			BeginTransaction(IsolationLevel.ReadCommitted, CommandExecution.Immediate);
		}
		public void BeginTransaction(IsolationLevel isoLevel) 
		{
			BeginTransaction(isoLevel, CommandExecution.Immediate);
		}
		public void BeginTransaction(CommandExecution execution) 
		{
			BeginTransaction(IsolationLevel.ReadCommitted, execution);
		}
		public void BeginTransaction(IsolationLevel isoLevel, CommandExecution execution)
		{
			_isInTransaction = true;
			_isolationLevel = isoLevel;
			_execution = execution;
		}

		public int CommitTransaction()
		{
			int recordsAffected = 0;

			if(!_isInTransaction)
				throw new DataSourceException("Not in transction!");

			if(_execution == CommandExecution.NoOutputQueued) 
			{
				if(_queuedCommands.Count > 0)
				{
					recordsAffected = FlushTransaction();
				}
			}

			foreach(IDbTransaction tran in _dbTransactionsByDbConnection.Values) 
			{
				tran.Commit();
			}
			_dbTransactionsByDbConnection.Clear();
			foreach(IDbConnection con in _dbConnectionsByDataSource.Values) 
			{
				con.Close();
			}
			_dbConnectionsByDataSource.Clear();

			_isInTransaction = false;

			return recordsAffected;
		}

		public void RollbackTransaction()
		{
			if(!_isInTransaction)
				throw new DataSourceException("Not in transction!");
			
			foreach(IDbTransaction tran in _dbTransactionsByDbConnection.Values) 
			{
				tran.Rollback();
			}
			_dbTransactionsByDbConnection.Clear();
			foreach(IDbConnection con in _dbConnectionsByDataSource.Values) 
			{
				con.Close();
			}
			_dbConnectionsByDataSource.Clear();

			_isInTransaction = false;
		}

		public int FlushTransaction() 
		{
			int recordsAffected = 0;

			foreach(IDataCommand cmd in _queuedCommands) 
			{
				IDbConnection con = _dbConnectionsByDataSource[cmd.DataSource] as IDbConnection;
				IDbTransaction tran = null;
				if(con == null) 
				{
					con = cmd.DataSource.CreateConnection();
					_dbConnectionsByDataSource.Add(cmd.DataSource, con);
				}
				if(con.State == ConnectionState.Closed) 
				{
					con.Open();
				}
				if(_isInTransaction) 
				{
					tran = _dbTransactionsByDbConnection[con] as IDbTransaction;
					if(tran == null) 
					{
						tran = con.BeginTransaction(_isolationLevel);
						_dbTransactionsByDbConnection.Add(con, tran);
					}
				}
				recordsAffected += cmd.ExecuteNonQuery(con, tran);
			}
			_queuedCommands.Clear();
			
			return recordsAffected;
		}

		public bool IsInTransaction { get { return _isInTransaction; } }
		public CommandExecution Execution { get { return _execution; } }
		public IsolationLevel Isolation { get { return _isolationLevel; } }

	}
}
