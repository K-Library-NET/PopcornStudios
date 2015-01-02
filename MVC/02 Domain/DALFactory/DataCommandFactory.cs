using System;
using System.Collections;
using System.Xml;
using System.Data;
using System.IO;
using System.Reflection;

namespace BLS_AspNet.DALFactory
{
	/// <summary>
	/// Each DataSource has one DataCommandFactory and delegates work to it.
	/// Returns DataCommands from the cache.
	/// Maintains/refreshes internal DataCommand cache.
	/// Config info retrieved from CommandFileDir/CommandFileMask from the DataProvider.
	/// Sets and uses FileSystemWatcher.
	/// </summary>
	class DataCommandFactory
	{
		private DataSource _dataSource = null;
		private DataProvider _provider = null;
		private string _commandDir = "";
		private string _commandFileMask = "";

		private Hashtable _commands = null;
		private FileSystemWatcher _watcher = null;
		private Hashtable _configsLastChanged = null;

		public DataCommandFactory(
			DataSource dataSource, string commandDir, string commandFileMask)
		{
			_dataSource = dataSource;
			_provider = dataSource.Provider;
			_commandDir = commandDir;
			_commandFileMask = commandFileMask;

			//set the watcher
			_watcher = 
				new FileSystemWatcher(_commandDir, _commandFileMask);
			_watcher.NotifyFilter = NotifyFilters.LastWrite;
			_watcher.Changed += new FileSystemEventHandler(Config_Changed);
			_watcher.EnableRaisingEvents = true;

			_configsLastChanged = new Hashtable();
		}

        public IDataCommand GetCommand(string commandName) 
        //public IDataCommand GetTxtCommand(string commandName)
		{
			if(_commands == null) 
			{
				LoadDataSourceCommands();
			}

			DataCommand cmd = _commands[commandName] as DataCommand;
			if(cmd == null) 
			{
				throw new DataSourceException("DataCommand with name " + commandName + " not found.");
			}

			DataCommand newCmd = (DataCommand)cmd.Clone();

			return newCmd;
		}

		private void Config_Changed(object sender, FileSystemEventArgs e) 
		{
			DateTime configLastChanged = DateTime.MinValue;

			if(_configsLastChanged.Contains(e.FullPath)) 
			{
				configLastChanged = (DateTime)_configsLastChanged[e.FullPath];
			}
			TimeSpan timeDif = DateTime.Now - configLastChanged;

			if(timeDif.Seconds > 2) 
			{
				LoadDataSourceCommandsFromFile(e.FullPath);
				_configsLastChanged[e.FullPath] = DateTime.Now;
			}
		}

		private void LoadDataSourceCommandsFromFile(string configFileName) 
		{
			Hashtable commands = new Hashtable();

			XmlTextReader xr = new XmlTextReader(configFileName);

			xr.WhitespaceHandling = WhitespaceHandling.None;
			xr.MoveToContent();

			DataCommand currentDataCommand = null;
			IDbCommand currentDbCommand = null;
			bool isCommandTextCurrentNode = false;
				
			while(xr.Read())
			{
				switch(xr.NodeType) 
				{
					case XmlNodeType.Element:
					switch(xr.Name) 
					{
						case "dataCommand":
							//instantiate the command
							string commandName = xr.GetAttribute("name");

							currentDbCommand = 
								(IDbCommand)Activator.CreateInstance(_provider.CommandObjectType);

							string strCommandType = xr.GetAttribute("type");
							CommandType commandType = 
								(CommandType)Enum.Parse(typeof(CommandType), strCommandType , true);
							currentDbCommand.CommandType = commandType;

							currentDataCommand = new DataCommand(commandName, currentDbCommand, _dataSource);
							commands.Add(commandName, currentDataCommand);
							break;

						case "commandText":
							isCommandTextCurrentNode = true;
							break;

						case "param":
							IDbDataParameter param = currentDbCommand.CreateParameter();
							param.ParameterName = xr.GetAttribute("name");
							string strParamType = xr.GetAttribute("type");
							_provider.ParameterDbTypeProperty.SetValue(
								param, Enum.Parse(_provider.ParameterDbType, strParamType, true), null);
							string size = xr.GetAttribute("size");
							if(size != null)
								param.Size = Int32.Parse(xr.GetAttribute("size"));
							param.Direction = 
								(ParameterDirection)Enum.Parse(typeof(ParameterDirection), xr.GetAttribute("direction"));
							currentDbCommand.Parameters.Add(param);
							break;
					}
						break;

					case XmlNodeType.Text:
						if(isCommandTextCurrentNode) 
						{
							currentDbCommand.CommandText = xr.Value;
						}
						break;

					case XmlNodeType.EndElement:
					switch(xr.Name) 
					{
						case "commandText":
							isCommandTextCurrentNode = false;
							break;
					}
						break;
				}
			}
			xr.Close();
			
			foreach(DictionaryEntry entry in commands)
			{
				_commands[entry.Key] = entry.Value;
			}
		}

		private void LoadDataSourceCommands() 
		{
			_commands = new Hashtable();

			string[] fileNames = Directory.GetFiles(_commandDir, _commandFileMask);

			foreach(string fileName in fileNames) 
			{
				LoadDataSourceCommandsFromFile(fileName);
			}
		}

	}
}
