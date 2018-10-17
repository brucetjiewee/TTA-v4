using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TTA.Startup))]
namespace TTA
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
