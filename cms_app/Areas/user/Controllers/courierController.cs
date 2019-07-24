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
    public class courierController : baseClass
    {
        string result = string.Empty;

        // GET: user/courier
        public ActionResult Index(string prmId)
        {
            if (!String.IsNullOrEmpty(prmId))
            {
                int id = Int32.TryParse(prmId, out id) ? id : 0;
                //DataSet ds = new MasterDataLogic().GetDataWithCompanyForDropdown(id, "SAMPLECREATION");
                DataSet ds = new DesignerLogic().SaveCourierSubmition(prmId,"", "","","","","", "","", "","CREATE", out result);
                if (ds != null && ds.Tables.Count > 0)
                {
                    ViewData["dsData"] = ds;
                }
                ViewBag.MoldId = prmId;
            }
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
            DataSet ds = new DesignerLogic().SaveCourierSubmition("", "", "", "", "", "", "", "", "", "", "SELECT", out result);
            if(ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                if (dt != null && dt.Rows.Count > 0)
                    return PartialView(viewName, dt);
                else
                    return PartialView(viewName, null);
            }
            else
            {
                return PartialView(viewName, null);
            }
            //DesignerLogic st = new DesignerLogic();
            //DataTable dt = st.GetPendingSampleList("[Admin].[CourierSubmition_List]");
            //if (dt != null && dt.Rows.Count > 0)
            //    return PartialView(viewName, dt);
            //else
            //    return PartialView(viewName, null);
        }

        [SessionExpire]
        public JsonResult SaveResult()
        {
            try
            {
                #region FormData
                /*Parameters of Query Strings*/
                String prmMoldId = Request["hidId"];
                String prmCourierId = Request["courierId"];
                String prmTransporter = Request["Transporter"];
                String prmCourierNo = Request["CourierNo"];
                String prmSamplePhoto = Request["sample"];
                String prmRemarks = Request["remarks"];
                String prmAction = Request["action"];
                String prmStatus = Request["status"];
                String prmDesignerId = Request["designerId"];
                String prmAssignRemarks = Request["assignRemarks"];
                String prmAssignDate = Request["assignDate"];
                #endregion

                #region FileUpload
                if (Request.Files.Count > 0)
                {
                    string folderpath = Server.MapPath("~/images/sample/");
                    HttpFileCollectionBase hfc = Request.Files;
                    // LOOP THROUGH EACH SELECTED FILE.
                    for (int i = 0; i <= hfc.Count - 1; i++)
                    {
                        HttpPostedFileBase hpf = hfc[i];
                        // CREATE A FILE ATTACHMENT.
                        hpf.SaveAs(folderpath + hpf.FileName);
                    }
                }
                #endregion

                DataSet ds = new DesignerLogic().SaveCourierSubmition(prmMoldId, prmCourierId, prmTransporter, prmCourierNo, prmSamplePhoto, 
                    prmRemarks, prmStatus, prmAssignRemarks, prmAssignDate, prmDesignerId, prmAction,  out result);
            }
            catch (Exception ex)
            {
                result = ex.ToString();
            }
            return Json(result);
        }
    }
}