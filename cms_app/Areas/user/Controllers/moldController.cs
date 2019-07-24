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
    public class moldController : baseClass
    {
        string result = string.Empty;

        // GET: user/moldnew
        [SessionExpire]
        public ActionResult Index(string prmId)
        {
            ViewBag.MoldId = (!string.IsNullOrEmpty(prmId)) ? prmId : string.Empty;
            int id = Int32.TryParse(prmId, out id) ? id : 0;
            DataSet ds = new MasterDataLogic().GetDataWithCompanyForDropdown(id, "MoldCreation");
            if (ds != null && ds.Tables.Count > 0)
            {
                ViewData["dsData"] = ds;
            }
            return View();
        }
        

        [SessionExpire]
        public ActionResult List()
        {
            return View();
        }

        [SessionExpire]
        public JsonResult SaveResult()
        {
            try
            {
                #region FormData
                /*Parameters of Query Strings*/
                String prmMoldId = Request["hidId"];
                String prmCompanyCode = Request["comp"];
                String prmMoldName = Request["moldCode"];

                String prmCustomerId = Request["customer"];
                String prmAddress = Request["customeraddress"];
                String prmContactPerson = Request["contactperson"];
                String prmContactNo = Request["contactpersonmobile"];

                String prmTechnology = Request["technology"];
                String prmSize = Request["size"];
                String prmColor = Request["color"];
                String prmDesignerId = Request["designer"];

                String prmCourierAddress = Request["address"];

                String prmLastSubmitionDate = Request["lastSubmitionDate"];
                String prmAssignDate = Request["assignDate"];
                String prmMoldPicture = Request["photoMold"];
                String prmTechPacDetail = Request["photoTech"];

                String prmRemarks = Request["remarks"];
                Boolean prmActive = Request["hidChecked"] == "1" ? true : false;
                String prmAction = Request["hidAction"];
                String prmIsOrder = Request["hidIsOrder"];
                
                #endregion

                #region FileUpload
                if (Request.Files.Count > 0)
                {
                    string folderpath = Server.MapPath("~/images/mold/");
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

                MoldLogic st = new MoldLogic();
                DataTable dt = st.NewMoldManage(
                    prmMoldId, prmCompanyCode, prmMoldName, prmCustomerId, prmAddress, prmContactPerson, prmContactNo, prmTechnology, prmSize, prmColor, prmDesignerId,
                    prmCourierAddress, prmLastSubmitionDate, prmAssignDate, prmMoldPicture, prmTechPacDetail, prmRemarks, prmActive, prmAction, prmIsOrder, out result);

            }
            catch (Exception ex)
            {
                result = ex.ToString();
            }
            return Json(result);
        }

        [SessionExpire]
        public ActionResult GetReport(string viewName, string searchType)
        {
            MoldLogic st = new MoldLogic();
            DataTable dt = st.GetMoldList(searchType, 1, 0);
            if (dt != null && dt.Rows.Count > 0)
                return PartialView(viewName, dt);
            else
                return PartialView(viewName, null);
        }

        [SessionExpire]
        //public string SearchByMoldAndDeveloperCode(string prmCode, string prmSearchBy)
        //{
        //    MoldLogic st = new MoldLogic();
        //    DataSet ds = st.SearchMoldDataByMoldCode(prmCode, prmSearchBy);
        //    //var obj = Json(ds, JsonRequestBehavior.AllowGet);
        //    return JsonConvert.SerializeObject(ds, Formatting.Indented);
        //}
        public string GetMasterListForEdit(string prmId)
        {
            MasterDataLogic st = new MasterDataLogic();
            DataTable dt = st.GetMasterListForEdit(prmId, "[Admin].[Master_Mold]");
            return JsonConvert.SerializeObject(dt, Formatting.Indented);
        }
    }
}