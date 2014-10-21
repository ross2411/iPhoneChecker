using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(iPhoneCheckerWeb.Startup))]
namespace iPhoneCheckerWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
