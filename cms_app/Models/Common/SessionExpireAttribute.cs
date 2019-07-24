using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;

namespace cms_app.Models.Common
{
    public class SessionExpireAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            String strDefaultUrl = string.Empty;
            HttpContext ctx = HttpContext.Current;

            if (ConfigurationManager.AppSettings["defaultLogOffURL"] != null)
                strDefaultUrl = ConfigurationManager.AppSettings["defaultLogOffURL"].ToString();
            else
                strDefaultUrl = "http://dcs.hashtechservices.com";

            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                // check  sessions here
                if (HttpContext.Current.Session["userInfo"] == null)
                {
                    filterContext.Result = new JsonResult
                    {
                        Data = new
                        {
                            message = strDefaultUrl
                        },
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };
                }
            }
            else
            {
                // check  sessions here
                if (HttpContext.Current.Session["userInfo"] == null)
                {
                    filterContext.Result = new RedirectResult(strDefaultUrl);
                    return;
                }

                base.OnActionExecuting(filterContext);
            }
        }
    }
}