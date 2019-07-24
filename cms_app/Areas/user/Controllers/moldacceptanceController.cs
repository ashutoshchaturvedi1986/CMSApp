using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using cms_app.Areas.admin.Models;
using cms_app.Areas.user.Models;
using System.Data;
using Newtonsoft.Json;
using cms_app.Models.Common;

namespace cms_app.Areas.user.Controllers
{
    public class moldacceptanceController : baseClass
    {
        string result = string.Empty;

        // GET: user/newmoldorder
        public ActionResult Index(string code)
        {
            ViewBag.MoldId = (!string.IsNullOrEmpty(code)) ? code : string.Empty;
            DataSet ds = new MasterDataLogic().GetListofAllAddedMasterDataForEdit(code, "[Admin].[Master_Mold_NewOrder]");
            ViewData["dsData"] = ds;
            return View();
        }

        // GET: user/newmoldorder
        public ActionResult list()
        {
            return View();
        }

        [SessionExpire]
        public ActionResult GetReport(string viewName)
        {
            DataTable dt = new MasterDataLogic().GetListofAllAddedMasterData("GetListOfOrderdMold", 1, 0);
            if (dt != null && dt.Rows.Count > 0)
                return PartialView(viewName, dt);
            else
                return PartialView(viewName, null);
        }
    }
}