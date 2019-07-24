using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using cms_app.Areas.admin.Models;
using System.Data;
using cms_app.Models.Common;

namespace cms_app.Areas.admin.Controllers
{
    public class colourController : baseClass
    {
        string result = string.Empty;

        // GET: admin/COLOUR
        [SessionExpire]
        public ActionResult Index()
        {
            DataSet ds = new MasterDataLogic().MasterDataByCategory1("", "", "", "COLOUR", "", "", 1, 0, "CREATE", out result);
            if (ds != null && ds.Tables.Count > 0)
                ViewData["dtCompany"] = ds.Tables[0];
            return View();
        }

        [SessionExpire]
        public ActionResult Edit()
        {
            string colorCode = Request.Form["code"];
            string compCode = Request.Form["compcode"];
            DataSet ds = null;
            if (!string.IsNullOrEmpty(colorCode))
            {
                ds = new MasterDataLogic().MasterDataByCategory1(compCode, colorCode, "", "COLOUR", "", "", 1, 0, "EDIT", out result);
            }
            ViewData["dsData"] = ds;
            return View(ds);
        }
        

        [SessionExpire]
        public ActionResult List()
        {
            return View();
        }

        [SessionExpire]
        public JsonResult SaveResult(String prmCompanyCode, String prmMDDCode, String prmMDDName, String prmRemarks, Int32 prmActive, String prmAction)
        {
            DataTable dt = new MasterDataLogic().MasterDataByCategory(prmCompanyCode, prmMDDCode, "", "COLOUR", prmMDDName, prmRemarks, prmActive, 0, prmAction, out result);
            return Json(result);
        }

        [SessionExpire]
        public ActionResult GetReport(string viewName)
        {
            DataTable dt = new MasterDataLogic().MasterDataByCategory("", "", "", "COLOUR", "", "", -1, -1, "SELECT", out result);
            if (dt != null && dt.Rows.Count > 0)
                return PartialView(viewName, dt);
            else
                return PartialView(viewName, null);
        }
    }
}