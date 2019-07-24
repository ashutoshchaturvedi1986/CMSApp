using System;
using System.Web.Mvc;
using cms_app.Areas.admin.Models;
using System.Data;
using Newtonsoft.Json;
using cms_app.Models.Common;

namespace cms_app.Areas.admin.Controllers
{
    public class dashboardController : baseClass
    {
        // GET: admin/dashboard
        [SessionExpire]
        public ActionResult Index()
        {
            string userRole = "1";
            if (Session["userInfo"] != null)
            {
                cms_app.Models.LoginModalData dm = (cms_app.Models.LoginModalData)Session["userInfo"];
                userRole = dm.userRole.ToUpper();
                if (userRole == "DESIGNER" || userRole == "ADMIN")
                {
                    ViewData["dtDashboard"] = new DashboardLogic().GetDashboardReport();
                }
            }
            return View();
        }
    }
}