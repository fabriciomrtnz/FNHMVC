using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FNHMVC.Web.Startup))]
namespace FNHMVC.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
