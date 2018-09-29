using System.Web.Mvc;

namespace White.Admin.Areas.JX3
{
    public class JX3AreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "JX3";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "JX3_default",
                "JX3/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new string[] { "White.Admin.Areas.JX3.Controllers" }
            );
        }
    }
}