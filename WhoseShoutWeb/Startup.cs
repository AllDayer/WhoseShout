using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(WhoseShoutWeb.Startup))]

namespace WhoseShoutWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureMobileApp(app);
        }
    }
}