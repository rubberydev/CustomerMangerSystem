using System.Web.Mvc;
using System.Web.Routing;

namespace Ingeneo.AppContratos
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{*url}", new { url = @".*\.asmx(/.*)?" });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Clientes", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
