using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;

namespace cms_app.Areas.admin.Controllers
{
    public class baseClass : Controller
    {
        public JsonResult CheckSession()
        {
            JsonResult jr = new JsonResult();
            if (System.Web.HttpContext.Current.Session["userInfo"] == null)
            {
                String strDefaultUrl = string.Empty;
                if (ConfigurationManager.AppSettings["defaultLogOffURL"] != null)
                {
                    strDefaultUrl = ConfigurationManager.AppSettings["defaultLogOffURL"].ToString();
                }
                else
                    strDefaultUrl = "http://dcs.hashtechservices.com";
                jr.Data = strDefaultUrl;
            }
            else
            {
                jr.Data = "Yes";
            }
            return jr;
        }
    }
}