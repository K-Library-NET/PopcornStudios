using System.Reflection;
using System.Configuration;

namespace BLS_AspNet.DALFactory
{
    /// <summary>
    /// 提供了创建各种数据对象DAL的方法，这个类是抽象工厂后推行的模式，取代了前面创建的工厂类体系
    /// 从配置文件中指定
    /// </summary>
    public sealed class DataAccess
    {
        // Look up the DAL implementation we should be using
        private static readonly string path = ConfigurationManager.AppSettings["WebDAL"];

        private DataAccess() { }

        public static BLS_AspNet.IDAL.ICOM_USER_INFO CreateComUserInfo()
        {
            string className = path + ".DalCOM_USER_INFO";
            return (BLS_AspNet.IDAL.ICOM_USER_INFO)Assembly.Load(path).CreateInstance(className);
        }
    }
}
