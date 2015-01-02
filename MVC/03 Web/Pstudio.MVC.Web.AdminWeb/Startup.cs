using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Pstudio.MVC.Web.AdminWeb.Startup))]
namespace Pstudio.MVC.Web.AdminWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
