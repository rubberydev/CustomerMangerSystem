using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Ingeneo.AppContratos.Startup))]
namespace Ingeneo.AppContratos
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
