using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using cms_app.Areas.admin.Models;
using System.Data;
using Newtonsoft.Json;
using cms_app.Models.Common;
using System.IO;

namespace cms_app.Areas.admin.Controllers
{
    public class testController : baseClass
    {
        string result = string.Empty;

        // GET: admin/test
        public ActionResult Index()
        {
            DataSet ds = new MasterDataLogic().GetDataWithCompanyForDropdown(0, "MaterialCreation");
            if (ds != null && ds.Tables.Count > 0)
            {
                ViewData["dtCompany"] = ds.Tables[0];
                if (ds.Tables.Count >= 1 && ds.Tables[1].Rows.Count > 0)
                    ViewData["dtMaterialGroup"] = ds.Tables[1];
                if (ds.Tables.Count >= 1 && ds.Tables[2].Rows.Count > 0)
                    ViewData["dtUnit"] = ds.Tables[2];
            }

            return View();
        }

        [SessionExpire]
        public JsonResult SaveResult()
        {
            /*Parameters of Query Strings*/
            Int32 prmMaterialCreationId = Convert.ToInt32(Request["hidId"]);
            Int32 prmMaterialGroupId = Convert.ToInt32(Request["sltMaterialGroup"]);
            String prmMaterialName = Request["txtName"];
            Int32 prmUnitId = Convert.ToInt32(Request["sltUnit"]);
            String prmCompanyCode = Request["sltCompany"];

            String prmReOrderLevel = Request["txtReorder"];
            Decimal prmOpeningStock = Convert.ToDecimal(Request["txtOpeningStock"]);
            String prmRemarks = Request["txtRemarks"];
            Boolean prmActive = Request["hidChecked"] == "1" ? true : false;
            String prmAction = Request["hidAction"];

            string prmPhotoPath = string.Empty;

            if (Request.Files.Count > 0)
            {
                HttpFileCollectionBase hfc = Request.Files;
                // LOOP THROUGH EACH SELECTED FILE.
                for (int i = 0; i <= hfc.Count - 1; i++)
                {
                    HttpPostedFileBase hpf = hfc[i];
                    // CREATE A FILE ATTACHMENT.
                    string folderpath = Server.MapPath("~/materialimages/");
                    prmPhotoPath = folderpath + hpf.FileName;
                    hpf.SaveAs(prmPhotoPath);
                }
            }

            MaterialLogic st = new MaterialLogic();
            DataTable dt = st.MaterialCreationMaster(prmMaterialCreationId, prmMaterialGroupId, prmMaterialName,
                prmUnitId, prmCompanyCode, prmPhotoPath, prmReOrderLevel, prmOpeningStock,
                 prmRemarks, prmActive, prmAction, out result);
            return Json(result);
        }
    }
}