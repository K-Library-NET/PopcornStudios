using System.Reflection;
using System.Configuration;

namespace BLS_AspNet.DALFactory
{
    /// <summary>
    /// �ṩ�˴����������ݶ���DAL�ķ�����������ǳ��󹤳������е�ģʽ��ȡ����ǰ�洴���Ĺ�������ϵ
    /// �������ļ���ָ��
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
