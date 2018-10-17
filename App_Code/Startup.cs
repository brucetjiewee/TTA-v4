using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ttav4.Startup))]
namespace ttav4
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
