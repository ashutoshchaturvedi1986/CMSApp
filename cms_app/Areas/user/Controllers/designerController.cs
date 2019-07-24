using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using cms_app.Areas.admin.Models;
using cms_app.Areas.user.Models;
using System.Data;
using Newtonsoft.Json;
using cms_app.Models.Common;

namespace cms_app.Areas.user.Controllers
{
    public class designerController : baseClass
    {
        string result = string.Empty;

        // GET: user/article
        [SessionExpire]
        public ActionResult Index(string prmId)
        {
            if (!String.IsNullOrEmpty(prmId))
            {
                int id = Int32.TryParse(prmId, out id) ? id : 0;
                DataSet ds = new MasterDataLogic().GetDataWithCompanyForDropdown(id, "SAMPLECREATION");
                if (ds != null && ds.Tables.Count > 0)
                {
                    ViewData["dsData"] = ds;
                }
                ViewBag.MoldId = prmId;
            }
            //ViewBag.MoldId = (!string.IsNullOrEmpty(prmId)) ? prmId : string.Empty;
            return View();
        }


        [SessionExpire]
        public ActionResult List()
        {
            return View();
        }

        [SessionExpire]
        public ActionResult GetReport(string viewName)
        {
            DesignerLogic st = new DesignerLogic();
            DataTable dt = st.GetPendingSampleList("[Admin].[SampleSubmition_List]");
            if (dt != null && dt.Rows.Count > 0)
                return PartialView(viewName, dt);
            else
                return PartialView(viewName, null);
        }

        [SessionExpire]
        public JsonResult SaveResult()
        {
            try
            {
                #region FormData
                /*Parameters of Query Strings*/
                String prmMoldId = Request["hidId"];
                String prmLastSubmitionDate = Request["lastSubmitionDate"];
                String prmAssignDate = Request["assignDate"];
                String prmRemarks = Request["remarks"];
                String prmIsApprove = Request["isApprove"];
                #endregion

                DataTable dt = new DesignerLogic().SaveSampleSubmition(prmMoldId, prmLastSubmitionDate, prmAssignDate, prmRemarks, prmIsApprove, out result);
            }
            catch (Exception ex)
            {
                result = ex.ToString();
            }
            return Json(result);
        }


        [SessionExpire]
        public string GetMasterListForEdit(string prmId)
        {
            MasterDataLogic st = new MasterDataLogic();
            DataTable dt = st.GetMasterListForEdit(prmId, "[Admin].[Master_Mold]");
            return JsonConvert.SerializeObject(dt, Formatting.Indented);
        }
    }
}