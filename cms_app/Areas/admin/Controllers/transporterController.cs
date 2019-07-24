using System;
using System.Web.Mvc;
using cms_app.Areas.admin.Models;
using System.Data;
using Newtonsoft.Json;
using cms_app.Models.Common;

namespace cms_app.Areas.admin.Controllers
{
    public class transporterController : baseClass
    {
        string result = string.Empty;

        // GET: admin/employee
        [SessionExpire]
        public ActionResult Index()
        {
            DataSet ds = new MasterDataLogic().GetDataWithCompanyForDropdown(0, "[Admin].[Transporter]");
            if (ds != null && ds.Tables.Count > 0)
                ViewData["dtCompany"] = ds.Tables[0];
            return View();
        }

        // EDIT: admin/state
        [SessionExpire]
        public ActionResult Edit()
        {
            string transId = Request.Form["code"];
            DataSet ds = null;
            if (!string.IsNullOrEmpty(transId))
                ds = new MasterDataLogic().GetListofAllAddedMasterDataForEdit(transId, "[Admin].[Transporter]");
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
        public JsonResult SaveResult(String prmTransporterId, String prmTransporterCode, String prmTransporterName, String prmCompanyCode, String prmAddress,
            String prmGSTNo, String prmPANNo, String prmContactPerson, String prmContactNo, String prmWebsite, String prmEmailID,
            String prmRemark, bool prmActive, String prmAction)
        {
            TransporterLogic st = new TransporterLogic();
            DataTable dt = st.TransporterManage(prmTransporterId, prmTransporterCode, prmTransporterName, prmCompanyCode, prmAddress, prmGSTNo, prmPANNo, prmContactPerson,
                prmContactNo, prmWebsite, prmEmailID, prmRemark, prmActive, prmAction, out result);
            return Json(result);
        }

        [SessionExpire]
        public ActionResult GetReport(string viewName)
        {
            DataTable dt = new MasterDataLogic().GetListofAllAddedMasterData("[Admin].[Transporter]", 1, 0);
            if (dt != null && dt.Rows.Count > 0)
                return PartialView(viewName, dt);
            else
                return PartialView(viewName, null);
        }
    }
}