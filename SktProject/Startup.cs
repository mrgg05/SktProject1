using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SktProject.Startup))]
namespace SktProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
