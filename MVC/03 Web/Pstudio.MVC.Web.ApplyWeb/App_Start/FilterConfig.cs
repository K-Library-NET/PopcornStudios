using System.Web;
using System.Web.Mvc;

namespace Pstudio.MVC.Web.ApplyWeb
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
