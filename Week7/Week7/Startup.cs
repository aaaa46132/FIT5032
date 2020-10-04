using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Week7.Startup))]
namespace Week7
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
