using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Pstudio.MVC.Web.ApplyWeb.Startup))]
namespace Pstudio.MVC.Web.ApplyWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
