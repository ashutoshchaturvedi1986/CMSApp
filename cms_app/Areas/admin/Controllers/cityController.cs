using cms_app.Areas.admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Web.UI.WebControls;
using cms_app.Models.Common;

namespace cms_app.Areas.admin.Controllers
{
    public class cityController : baseClass
    {
        string result = string.Empty;

        // GET: admin/city
        [SessionExpire]
        public ActionResult Index()
        {
            ViewData["dtState"] = new CityLogic().CityManage("", "", "","", true, "Select", out result);
            return View();
        }

        // EDIT: admin/state
        [SessionExpire]
        public ActionResult Edit()
        {
            string cityId = Request.Form["code"];
            DataSet ds = null;
            if (!string.IsNullOrEmpty(cityId))
            {
                ds = new MasterDataLogic().GetListofAllAddedMasterDataForEdit(cityId, "[Admin].[Master_City]");
            }
            ViewData["dsData"] = ds;
            return View(ds);
        }

        // Save: admin/state
        [SessionExpire]
        public ActionResult List()
        {
            return View();
        }

        [SessionExpire]
        public JsonResult SaveResult(String prmCityId, String prmStateId, String prmName, String prmRemarks, bool prmActive, String prmAction)
        {
            CityLogic st = new CityLogic();
            DataTable dt = st.CityManage(prmCityId, prmStateId, prmName, prmRemarks, prmActive, prmAction, out result);
            return Json(result);
        }

        [SessionExpire]
        public ActionResult GetReport(string viewName)
        {
            DataTable dt = new MasterDataLogic().GetListofAllAddedMasterData("[Admin].[Master_City]", 1, 0);
            if (dt != null && dt.Rows.Count > 0)
                return PartialView(viewName, dt);
            else
                return PartialView(viewName, null);
        }
    }
}