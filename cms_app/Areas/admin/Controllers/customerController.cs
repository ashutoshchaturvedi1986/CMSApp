using System;
using System.Web.Mvc;
using cms_app.Areas.admin.Models;
using System.Data;
using Newtonsoft.Json;
using cms_app.Models.Common;

namespace cms_app.Areas.admin.Controllers
{
    public class customerController : baseClass
    {
        string result = string.Empty;

        // GET: admin/employee
        [SessionExpire]
        public ActionResult Index()
        {
            DataSet ds = new MasterDataLogic().GetDataWithCompanyForDropdown(0, "[Admin].[Customer]");
            if (ds != null && ds.Tables.Count > 0)
                ViewData["dtCompany"] = ds.Tables[0];
            return View();
        }

        // EDIT: admin/customer
        [SessionExpire]
        public ActionResult Edit()
        {
            string cusId = Request.Form["code"];
            DataSet ds = null;
            if (!string.IsNullOrEmpty(cusId))
                ds = new MasterDataLogic().GetListofAllAddedMasterDataForEdit(cusId, "[Admin].[Customer]");
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
        public JsonResult SaveResult(String prmCustomerId, String prmCustomerCode, String prmCustomerName, String prmCompanyCode, String prmAddress,
            String prmGSTNo, String prmPANNo, String prmBillingAddress, String prmContactPerson, String prmContactNo, String prmWebsite, String prmEmailID,
            String prmRemark, bool prmActive, String prmAction)
        {
            CustomerLogic st = new CustomerLogic();
            DataTable dt = st.CustomerManage(prmCustomerId, prmCustomerCode, prmCustomerName, prmCompanyCode, prmAddress,
                prmGSTNo, prmPANNo, prmBillingAddress, prmContactPerson, prmContactNo, prmWebsite, prmEmailID,
                prmRemark, prmActive, prmAction, out result);
            return Json(result);
        }

        [SessionExpire]
        public ActionResult GetReport(string viewName)
        {
            DataTable dt = new MasterDataLogic().GetListofAllAddedMasterData("[Admin].[Customer]", 1, 0);
            if (dt != null && dt.Rows.Count > 0)
                return PartialView(viewName, dt);
            else
                return PartialView(viewName, null);
        }
    }
}