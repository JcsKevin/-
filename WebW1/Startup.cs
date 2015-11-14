using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebW1.Startup))]
namespace WebW1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
