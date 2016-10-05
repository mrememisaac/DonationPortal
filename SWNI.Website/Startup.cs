using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SWNI.Website.Startup))]
namespace SWNI.Website
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
