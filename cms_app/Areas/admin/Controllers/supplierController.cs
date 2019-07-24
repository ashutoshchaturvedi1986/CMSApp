using System;
using System.Web.Mvc;
using cms_app.Areas.admin.Models;
using System.Data;
using Newtonsoft.Json;
using cms_app.Models.Common;

namespace cms_app.Areas.admin.Controllers
{
    public class supplierController : baseClass
    {
        string result = string.Empty;

        // GET: admin/employee
        [SessionExpire]
        public ActionResult Index()
        {
            DataSet ds = new MasterDataLogic().GetDataWithCompanyForDropdown(0, "[Admin].[Supplier]");
            if (ds != null && ds.Tables.Count > 0)
                ViewData["dtCompany"] = ds.Tables[0];
            return View();
        }

        // EDIT: admin/state
        [SessionExpire]
        public ActionResult Edit()
        {
            string supplierId = Request.Form["code"];
            DataSet ds = null;
            if (!string.IsNullOrEmpty(supplierId))
                ds = new MasterDataLogic().GetListofAllAddedMasterDataForEdit(supplierId, "[Admin].[Supplier]");
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
        public JsonResult SaveResult(String prmSupplierId, String prmSupplierCode, String prmSupplierName, String prmCompanyCode, String prmAddress,
            String prmGSTNo, String prmPANNo, String prmContactPerson, String prmContactNo, String prmWebsite, String prmEmailID,
            String prmRemark, bool prmActive, String prmAction)
        {
            SupplierLogic st = new SupplierLogic();
            DataTable dt = st.SupplierManage(prmSupplierId, prmSupplierCode, prmSupplierName, prmCompanyCode, prmAddress, prmGSTNo, prmPANNo, prmContactPerson, 
                prmContactNo,prmWebsite, prmEmailID,prmRemark, prmActive, prmAction, out result);
            return Json(result);
        }

        [SessionExpire]
        public ActionResult GetReport(string viewName)
        {
            DataTable dt = new MasterDataLogic().GetListofAllAddedMasterData("[Admin].[Supplier]", 1, 0);
            if (dt != null && dt.Rows.Count > 0)
                return PartialView(viewName, dt);
            else
                return PartialView(viewName, null);
        }
    }
}