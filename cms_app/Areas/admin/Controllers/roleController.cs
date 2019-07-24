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
    public class roleController : baseClass
    {
        string result = string.Empty;

        // GET: admin/brand
        [SessionExpire]
        public ActionResult Index()
        {
            //ViewData["dtCompany"] = new MasterDataLogic().GetMasterList("[Admin].[Company]", 1, 0);
            DataSet ds = new MasterDataLogic().MasterDataByCategory1("", "", "", "ROLE", "", "", 1, 0, "CREATE", out result);
            if (ds != null && ds.Tables.Count > 0)
                ViewData["dtCompany"] = ds.Tables[0];
            return View();
        }

        [SessionExpire]
        public ActionResult Edit()
        {
            string roleCode = Request.Form["code"];
            string compCode = Request.Form["compcode"];
            DataSet ds = null;
            if (!string.IsNullOrEmpty(roleCode))
            {
                ds = new MasterDataLogic().MasterDataByCategory1(compCode, roleCode, "", "ROLE", "", "", 1, 0, "EDIT", out result);
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
        public JsonResult SaveResult(String prmCompanyCode, String prmMDDCode, String prmMDDName, String prmRemarks, int prmActive, String prmAction)
        {
            DataTable dt = new MasterDataLogic().MasterDataByCategory(prmCompanyCode, prmMDDCode, "", "ROLE", prmMDDName, prmRemarks, prmActive, 0, prmAction, out result);
            return Json(result);
        }

        [SessionExpire]
        public ActionResult GetReport(string viewName)
        {
            DataTable dt = new MasterDataLogic().MasterDataByCategory("", "", "", "ROLE", "", "", -1, -1, "SELECT", out result);
            if (dt != null && dt.Rows.Count > 0)
                return PartialView(viewName, dt);
            else
                return PartialView(viewName, null);
        }

        //public JsonResult FillMaster(string prmCompanyId)
        //{
        //    DataTable dtCity = new DataTable();
        //    JsonResult jr = new JsonResult();
        //    dtCity = new CityLogic().GetCityByStateId(prmCompanyId);
        //    List<CityModal> myClass = new List<CityModal>();
        //    foreach (DataRow row in dtCity.Rows)
        //        myClass.Add(new CityModal() { CityId = row["CityId"].ToString(), Name = row["Name"].ToString() });
        //    jr.Data = myClass;
        //    return jr;
        //}
    }
}