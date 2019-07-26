using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LabMVC.Startup))]
namespace LabMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
