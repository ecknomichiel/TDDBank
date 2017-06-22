using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TDDBanking.Startup))]
namespace TDDBanking
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
