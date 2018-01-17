using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ICT_Portal.Startup))]
namespace ICT_Portal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
