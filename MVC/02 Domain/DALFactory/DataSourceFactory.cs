using System;
using System.Data;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Collections;
//using System.Windows.Forms;


namespace BLS_AspNet.DALFactory
{
    /// <summary>
    /// DataSourceFactory returns DataSource objects.
    /// Maintains/refreshes internal cache of DataSources.
    /// Config info retrieved from DAC2.dll.config
    /// Sets and uses FileSystemWatcher.
    /// </summary>
    public class DataSourceFactory
    {
        private const string DEFAULT_DATASOURCE_NAME = "DAC2_DEFAULT_DATASOURCE_NAME";
        private static Hashtable _dataSources = null;
        private static FileSystemWatcher _watcher = null;
        private static string _configDirectoryName =
            Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase.Replace(@"file:///", ""));
        private static string _configFileName =
            Path.GetFileName(Assembly.GetExecutingAssembly().CodeBase).Replace(".DLL", ".dll.config");
        private static DateTime _configLastChanged = DateTime.Now;

        //static constructor
        static DataSourceFactory()
        {
            //set up a watcher to trace events on the config file
            try
            {
                _watcher =
                    new FileSystemWatcher(_configDirectoryName, _configFileName);
                _watcher.NotifyFilter = NotifyFilters.LastWrite;
                _watcher.Changed += new FileSystemEventHandler(Config_Changed);
                _watcher.EnableRaisingEvents = true;
            }
            catch
            {
            }
        }

        //only static methods
        private DataSourceFactory() { }

        public static IDataSource GetDataSource()
        {
            return GetDataSource(DEFAULT_DATASOURCE_NAME);
        }

        public static string opname = "unknown";
        public static string opcode = "unknown";
        public static string Ldbmbh = "";
        public static int prnLeft = 0;
        public static int prnTop = 0;

        public static IDataSource GetDataSource(string dataSourceName)
        {
            if (_dataSources == null)
            {
                LoadDataSources(_configDirectoryName + Path.DirectorySeparatorChar + _configFileName);
            }

            DataSource ds = _dataSources[dataSourceName] as DataSource;

            if (ds == null)
            {
                //TODO: specialize the exception, perhaps DataSourceNotFoundException
                throw new DataSourceException("DataSource with name '" + dataSourceName + "' not found.");
            }
            if (ds.ConnString.ToLower().IndexOf("ora") >= 0)
                ds.ExecuteTxtNonQuery("ALTER SESSION SET NLS_DATE_FORMAT='YYYY-MM-DD HH24:MI:SS'");
            if (ds.Provider.ConnectionObjectType.Name.ToLower().IndexOf("ora") >= 0)
                ds.ExecuteTxtNonQuery("ALTER SESSION SET NLS_DATE_FORMAT='YYYY-MM-DD HH24:MI:SS'");

            return ds;
        }

        private static void Config_Changed(object sender, FileSystemEventArgs e)
        {
            TimeSpan timeDif = DateTime.Now - _configLastChanged;
            if (timeDif.Seconds > 2)
            {
                LoadDataSources(e.FullPath);
                _configLastChanged = DateTime.Now;
            }
        }

        private static void LoadDataSources(string configFileName)
        {
            XmlTextReader xr = new XmlTextReader(configFileName);

            xr.WhitespaceHandling = WhitespaceHandling.None;
            xr.MoveToContent();

            Hashtable dataProviders = new Hashtable();
            Hashtable dataSources = new Hashtable();
            string name = "";

            while (xr.Read())
            {
                switch (xr.NodeType)
                {
                    case XmlNodeType.Element:
                        switch (xr.Name)
                        {
                            case "dataProvider":
                                name = xr.GetAttribute("name");
                                Type connectionType = Type.GetType(xr.GetAttribute("connectionType"));
                                Type adapterType = Type.GetType(xr.GetAttribute("adapterType"));
                                Type commandType = Type.GetType(xr.GetAttribute("commandType"));
                                Type parameterType = Type.GetType(xr.GetAttribute("parameterType"));
                                Type parameterDbType = Type.GetType(xr.GetAttribute("parameterDbType"));
                                //MessageBox.Show(parameterDbType.Name);
                                PropertyInfo parameterDbTypeProperty =
                                    parameterType.GetProperty(xr.GetAttribute("parameterDbTypeProperty"));//, BindingFlags.Instance | BindingFlags.Public);

                                if (xr.GetAttribute("parameterDbTypeProperty") == "OralceType")
                                {
                                    //								MessageBox.Show("parameterDbTypeProperty=" + 
                                    //									xr.GetAttribute("parameterDbTypeProperty") + 
                                    //									",\nparameterType=" + xr.GetAttribute("parameterType") +
                                    //									",\nparameterDbType=" + xr.GetAttribute("parameterDbType"));
                                    if (parameterDbTypeProperty == null)
                                    {
                                        //MessageBox.Show("parameterDbTypeProperty is null");
                                        Type pType = Type.GetType("System.Data.OracleClient.OracleParameter, System.Data.OracleClient, Version=1.0.5000.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
                                        parameterDbTypeProperty = pType.GetProperty("OracleType");
                                    }
                                }


                                dataProviders.Add(name,
                                    new DataProvider(
                                    name, connectionType, commandType,
                                    parameterType, parameterDbType,
                                    parameterDbTypeProperty, adapterType));
                                break;

                            case "dataSource":
                                name = xr.GetAttribute("name");
                                string connectionString = xr.GetAttribute("connectionString");
                                string commandDir = _configDirectoryName;//xr.GetAttribute("commandDir");
                                string commandFileMask = xr.GetAttribute("commandFileMask");
                                DataProvider provider =
                                    (DataProvider)dataProviders[xr.GetAttribute("provider")];

                                DataSource ds =
                                    new DataSource(name, provider, connectionString,
                                        commandDir, commandFileMask);
                                dataSources.Add(ds.Name, ds);

                                string strDefault = xr.GetAttribute("default");
                                if (strDefault != null)
                                {
                                    bool isDefault = bool.Parse(strDefault);
                                    if (isDefault)
                                    {
                                        dataSources.Add(DEFAULT_DATASOURCE_NAME, ds);
                                    }
                                }

                                break;
                        }
                        break;
                }
            }
            xr.Close();

            _dataSources = dataSources;
        }
    }
}
