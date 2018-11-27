using System.Web.Mvc;

namespace OnlineEducation.Areas.HelpOnline
{
    public class HelpOnlineAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "HelpOnline";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "HelpOnline_default",
                "HelpOnline/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                 new string[] { "OnlineEducation.Areas.HelpOnline.Controllers" }
            );
        }
    }
}