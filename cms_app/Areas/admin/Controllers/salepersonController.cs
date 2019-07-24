using System;
using System.Web.Mvc;
using cms_app.Areas.admin.Models;
using System.Data;
using Newtonsoft.Json;
using cms_app.Models.Common;

namespace cms_app.Areas.admin.Controllers
{
    public class salepersonController : baseClass
    {
        string result = string.Empty;

        // GET: admin/employee
        [SessionExpire]
        public ActionResult Index()
        {
            DataSet ds = new MasterDataLogic().GetDataWithCompanyForDropdown(0, "[Admin].[SalePerson]");
            if (ds != null && ds.Tables.Count > 0)
                ViewData["dtCompany"] = ds.Tables[0];
            return View();
        }

        // EDIT: admin/state
        [SessionExpire]
        public ActionResult Edit()
        {
            string salePersonId = Request.Form["code"];
            DataSet ds = null;
            if (!string.IsNullOrEmpty(salePersonId))
            {
                ds = new MasterDataLogic().GetListofAllAddedMasterDataForEdit(salePersonId, "[Admin].[SalePerson]");
            }
            ViewData["dsData"] = ds;
            return View();
        }

        // GET: admin/employee
        [SessionExpire]
        public ActionResult List()
        {
            return View();
        }

        [SessionExpire]
        public JsonResult SaveResult(String prmSalePersonId, String prmSalePersonCode, String prmSalePersonName, String prmCompanyCode, String prmContactNo, String prmAddress,
            String prmDOB, String prmPanNo,  String prmAdharNo, String prmDOJ,String prmRemark, bool prmActive, String prmAction)
        {
            SalePersonLogic st = new SalePersonLogic();
            DataTable dt = st.SalePersonManage(prmSalePersonId, prmSalePersonCode, prmSalePersonName, prmCompanyCode, prmContactNo, prmAddress,
                prmDOB, prmPanNo, prmAdharNo, prmDOJ, prmRemark, prmActive, prmAction, out result);
            return Json(result);
        }

        [SessionExpire]
        public ActionResult GetReport(string viewName)
        {
            DataTable dt = new MasterDataLogic().GetListofAllAddedMasterData("[Admin].[SalePerson]", 1, 0);
            if (dt != null && dt.Rows.Count > 0)
                return PartialView(viewName, dt);
            else
                return PartialView(viewName, null);
        }
    }
}