using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace OnlineEducation
{
    public class RouteConfig
    {
        private static string namespaces;

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute("HelpOnline/Level1", "HelpOnline/Level1/{name}", new { controller = "Level1", action = "Index", name = UrlParameter.Optional });
            routes.MapRoute("HelpOnline/Level2", "HelpOnline/Level2/{name}", new {controller= "Level2", action="Index", name = UrlParameter.Optional });
            routes.MapRoute("HelpOnline/Level3", "HelpOnline/Level3/{name}", new { controller = "Level3", action = "Index", name = UrlParameter.Optional });
            routes.MapRoute("HelpOnline/AdminLevel1", "HelpOnline/AdminLevel1/{name}", new { controller = "AdminLevel1", action = "Index", name = UrlParameter.Optional });
            routes.MapRoute("HelpOnline/AdminLevel2", "HelpOnline/AdminLevel2/{name}", new { controller = "AdminLevel2", action = "Index", name = UrlParameter.Optional });
            routes.MapRoute("HelpOnline/UploadHelpOnlineXM", "HelpOnline/UploadHelpOnlineXM/{name}", new { controller = "UploadHelpOnlineXM", action = "Index", name = UrlParameter.Optional });
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
               namespaces: new string[] { "OnlineEducation.Controllers" }
            );
        }
    }
}
