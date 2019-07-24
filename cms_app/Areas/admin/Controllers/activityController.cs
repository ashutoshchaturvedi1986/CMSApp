using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using cms_app.Areas.admin.Models;
using System.Data;
using Newtonsoft.Json;
using cms_app.Models.Common;

namespace cms_app.Areas.admin.Controllers
{
    public class activityController : baseClass
    {
        string result = string.Empty;

        // GET: admin/activity
        [SessionExpire]
        public ActionResult Index()
        {
            DataSet ds = new MasterDataLogic().MasterDataByCategory1("", "", "", "ACT", "", "", 1, 0, "CREATE", out result);
            if (ds != null && ds.Tables.Count > 0)
            {
                ViewData["dtCompany"] = ds.Tables[0];
                if(ds.Tables.Count > 1)
                    ViewData["dtProcessGroup"] = ds.Tables[1];
            }
            return View();
        }

        [SessionExpire]
        public ActionResult Edit()
        {
            string actCode = Request.Form["code"];
            string compCode = Request.Form["compcode"];
            DataSet ds = null;
            if (!string.IsNullOrEmpty(actCode))
            {
                ds = new MasterDataLogic().MasterDataByCategory1(compCode, actCode, "", "ACT", "", "", 1, 0, "EDIT", out result);
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
        public JsonResult SaveResult(String prmCompanyCode, String prmMDDCode, String prmMDDName, String prmParrentMDDCode, String prmRemarks, Int32 prmActive, String prmAction)
        {
            DataTable dt = new MasterDataLogic().MasterDataByCategory(prmCompanyCode, prmMDDCode, prmParrentMDDCode, "ACT", prmMDDName, prmRemarks, prmActive, 0, prmAction, out result);
            return Json(result);
        }

        [SessionExpire]
        public ActionResult GetReport(string viewName)
        {
            DataTable dt = new MasterDataLogic().MasterDataByCategory("", "", "", "ACT", "", "", -1, -1, "SELECT", out result);
            if (dt != null && dt.Rows.Count > 0)
                return PartialView(viewName, dt);
            else
                return PartialView(viewName, null);
        }
    }
    //public class activityController : baseClass
    //{
    //    string result = string.Empty;

    //    // GET: admin/activity
    //    [SessionExpire]
    //    public ActionResult Index()
    //    {
    //        ViewData["dtCompany"] = new MasterDataLogic().GetMasterList("[Admin].[Company]", 1, 0);
    //        return View();
    //    }

    //    [SessionExpire]
    //    public ActionResult Edit()
    //    {
    //        string brandCode = Request.Form["code"];
    //        if (!string.IsNullOrEmpty(brandCode))
    //        {
    //            ViewData["dtCompany"] = new MasterDataLogic().GetMasterList("[Admin].[Company]", 1, 0);

    //            MasterDataLogic st = new MasterDataLogic();
    //            DataTable dt = st.MasterDataByCategory("", brandCode, "", "ACT", "", "", 1, 0, "EDIT", out result);
    //            if (dt != null && dt.Rows.Count > 0)
    //                return View(dt);
    //            else
    //                return View("List");
    //        }
    //        else
    //            return View("List");
    //    }

    //    [SessionExpire]
    //    public ActionResult List()
    //    {
    //        return View();
    //    }

    //    [SessionExpire]
    //    public JsonResult SaveResult(String prmCompanyCode, String prmMDDCode, String prmMDDName,String prmParrentMDDCode, String prmRemarks, Int32 prmActive, String prmAction)
    //    {
    //        MasterDataLogic st = new MasterDataLogic();
    //        DataTable dt = st.MasterDataByCategory(prmCompanyCode, prmMDDCode, prmParrentMDDCode, "ACT", prmMDDName, prmRemarks, prmActive, 0, prmAction, out result);
    //        return Json(result);

    //        //if (dt.Rows.Count > 0)
    //        //    return Json("Added/Updaet succesfully.");
    //        //else
    //        //    return Json("There is no data for upload");
    //    }

    //    [SessionExpire]
    //    public ActionResult GetReport(string viewName)
    //    {
    //        MasterDataLogic st = new MasterDataLogic();
    //        DataTable dt = st.MasterDataByCategory("", "", "", "ACT", "", "", -1, -1, "SELECT", out result);
    //        if (dt != null && dt.Rows.Count > 0)
    //            return PartialView(viewName, dt);
    //        else
    //            return PartialView(viewName, null);
    //    }

    //    [SessionExpire]
    //    public string FillHierarchy(string prmCompanyCode)
    //    {
    //        string JSONresult = string.Empty;
    //        DataSet dsHierarchy = new DataSet();
    //        EmployeeModal emp = new EmployeeModal();
    //        dsHierarchy = emp.GetAllMasterDataByCompanyId(prmCompanyCode,"PGM");
    //        JSONresult = JsonConvert.SerializeObject(dsHierarchy);

    //        return JSONresult;
    //    }
    //}
}