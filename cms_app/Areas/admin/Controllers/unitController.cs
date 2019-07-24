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
    public class unitController : baseClass
    {
        string result = string.Empty;

        // GET: admin/brand
        [SessionExpire]
        public ActionResult Index()
        {
            //ViewData["dtCompany"] = new MasterDataLogic().GetMasterList("[Admin].[Company]", 1, 0);
            //return View();
            DataSet ds = new MasterDataLogic().GetDataWithCompanyForDropdown(0, "[Admin].[Department]");
            if (ds != null && ds.Tables.Count > 0)
            {
                ViewData["dtCompany"] = ds.Tables[0];
            }
            return View();
        }

        [SessionExpire]
        public ActionResult Edit()
        {
            string unitId = Request.Form["code"];
            DataSet ds = null;
            if (!string.IsNullOrEmpty(unitId))
            {
                ds = new MasterDataLogic().GetListofAllAddedMasterDataForEdit(unitId, "[Admin].[Unit]");
            }
            ViewData["dsData"] = ds;
            return View();
            //string empId = Request.Form["code"];
            //if (!string.IsNullOrEmpty(empId))
            //{
            //    ViewData["dtCompany"] = new MasterDataLogic().GetMasterList("[Admin].[Company]", 1, 0);
            //    MasterDataLogic st = new MasterDataLogic();
            //    DataTable dt = st.GetMasterListForEdit(empId, "[Admin].[Unit]");
            //    if (dt != null && dt.Rows.Count > 0)
            //        return View(dt);
            //    else
            //        return View("List");
            //}
            //else
            //    return View("List");
        }

        [SessionExpire]
        public ActionResult List()
        {
            return View();
        }

        [SessionExpire]
        public JsonResult SaveResult(Int32 prmUnitId, String prmUnitCode, String prmName, String prmCompanyCode, Int32 prmDecimal, String prmRemark, bool prmActive, String prmAction)
        {
            DataTable dt = new UnitLogic().UnitManage(prmUnitId, prmUnitCode, prmName, prmCompanyCode, prmDecimal, prmRemark, prmActive, prmAction, out result);
            return Json(result);
        }

        [SessionExpire]
        public ActionResult GetReport(string viewName)
        {
            //MasterDataLogic st = new MasterDataLogic();
            //DataTable dt = st.GetMasterList("[Admin].[Unit]", 1, 0);
            //if (dt != null && dt.Rows.Count > 0)
            //    return PartialView(viewName, dt);
            //else
            //    return PartialView(viewName, null);
            DataTable dt = new MasterDataLogic().GetListofAllAddedMasterData("[Admin].[Unit]", 1, 0);
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