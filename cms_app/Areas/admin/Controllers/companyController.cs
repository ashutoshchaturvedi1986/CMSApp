using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using cms_app.Areas.admin.Models;
using cms_app.Models.Common;

namespace cms_app.Areas.admin.Controllers
{
    public class companyController : baseClass
    {
        string result = string.Empty;

        // GET: admin/company
        [SessionExpire]
        public ActionResult Index()
        {
            ViewData["dtState"] = new StateLogic().StateManage("", "", "", true, "Select", out result);
            return View();
        }

        // EDIT: admin/state
        [SessionExpire]
        public ActionResult Edit()
        {
            string code = Request.Form["code"];
            DataSet ds = null;
            if (!string.IsNullOrEmpty(code))
            {
                ds = new MasterDataLogic().GetListofAllAddedMasterDataForEdit(code, "[Admin].[Company]");
            }
            ViewData["dsData"] = ds;
            return View(ds);
        }

        [SessionExpire]
        public ActionResult List()
        {
            return View();
        }

        // Bind Month of Selected Year
        [SessionExpire]
        public JsonResult FillCity(string prmStateId)
        {
            DataTable dtCity = new DataTable();
            JsonResult jr = new JsonResult();
            dtCity = new CityLogic().GetCityByStateId(prmStateId);
            List<CityModal> myClass = new List<CityModal>();
            foreach (DataRow row in dtCity.Rows)
                myClass.Add(new CityModal() { CityId = row["CityId"].ToString(), Name = row["Name"].ToString() });
            jr.Data = myClass;
            return jr;
        }

        [SessionExpire]
        public JsonResult SaveResult(
            String prmCompanyName, String prmCompanyCode, String prmAlias, String prmStateId, String prmCityId, String prmRegAddress,
            String prmBankName, String prmBankAcctNo, String prmBranchName, String prmPANNo, String prmMICRNo, String prmGSTNo,
            String prmContactEmail, String prmPhone, String prmFax, String prmWebsite,String prmContactPerson,String prmMobileNo, bool prmActive, String prmAction
        )
        {
            DataTable dt = new CompanyModal().CompanyManage(
                prmCompanyCode, prmCompanyName, prmAlias, prmStateId, prmCityId, prmRegAddress,prmBankName, prmBranchName, prmBankAcctNo, prmPANNo, 
                prmMICRNo, prmGSTNo, prmContactEmail, prmPhone, prmFax, prmWebsite,prmContactPerson, prmMobileNo,prmActive, prmAction, out result);

            return Json(result);
        }

        [SessionExpire]
        public ActionResult GetReport(string viewName)
        {
            DataTable dt = new MasterDataLogic().GetListofAllAddedMasterData("[Admin].[Company]", 1, 0);
            if (dt != null && dt.Rows.Count > 0)
                return PartialView(viewName, dt);
            else
                return PartialView(viewName, null);
        }
    }
}