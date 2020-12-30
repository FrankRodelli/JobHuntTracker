using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(JobHuntTrackerASP.Startup))]
namespace JobHuntTrackerASP
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
