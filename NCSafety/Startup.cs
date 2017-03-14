using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NCSafety.Startup))]
namespace NCSafety
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
