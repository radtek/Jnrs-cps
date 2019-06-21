using System.Web.Mvc;

namespace Learun.Application.Web.Areas.LR_RunParam
{
    public class LR_RunParamAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "LR_RunParam";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "LR_RunParam_default",
                "LR_RunParam/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}