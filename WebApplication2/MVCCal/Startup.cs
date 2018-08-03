using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVCCal.Startup))]
namespace MVCCal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
