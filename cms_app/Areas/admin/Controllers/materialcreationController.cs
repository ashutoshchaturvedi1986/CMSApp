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
    public class materialcreationController : baseClass
    {
        string result = string.Empty;

        // GET: admin/materialgroup
        public ActionResult Index()
        {
            DataSet ds = new MasterDataLogic().GetDataWithCompanyForDropdown(0, "MaterialCreation");
            if (ds != null && ds.Tables.Count > 0)
            {
                ViewData["dtCompany"] = ds.Tables[0];
                if (ds.Tables.Count >= 1 && ds.Tables[1].Rows.Count>0)
                    ViewData["dtMaterialGroup"] = ds.Tables[1];
                if (ds.Tables.Count >= 1 && ds.Tables[2].Rows.Count > 0)
                    ViewData["dtUnit"] = ds.Tables[2];
            }
            return View();
        }

        // EDIT: admin/state
        [SessionExpire]
        public ActionResult Edit()
        {
            string materialId = Request.Form["code"];
            DataSet ds = null;
            if (!string.IsNullOrEmpty(materialId))
            {
                ds = new MasterDataLogic().GetListofAllAddedMasterDataForEdit(materialId, "[Admin].[Master_MaterialCreation]");
            }
            ViewData["dsData"] = ds;
            return View();
        }

        // Save: admin/state
        [SessionExpire]
        public ActionResult List()
        {
            return View();
        }

        ///// <summary>
        ///// Upload selected Excel files.
        ///// </summary>
        //[SessionExpire]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult UploadExcelFile(HttpPostedFileBase files, FormCollection form)
        //{
        //    if (files != null && files.ContentLength > 0)
        //    {
        //        string fl = new FileInfo(files.FileName).Name;

        //        /*Parameters of Query Strings*/
        //        Int32 prmMaterialCreationId = Convert.ToInt32(Request["hidId"]);
        //        Int32 prmMaterialGroupId = Convert.ToInt32(Request["sltMaterialGroup"]);
        //        String prmMaterialName = Request["txtName"];
        //        Int32 prmUnitId = Convert.ToInt32(Request["sltUnit"]);
        //        String prmCompanyCode = Request["sltCompany"];

        //        String prmReOrderLevel = Request["txtReorder"];
        //        Decimal prmOpeningStock = Convert.ToDecimal(Request["txtOpeningStock"]);
        //        String prmRemarks = Request["txtRemarks"];
        //        Boolean prmActive = Request["hidChecked"] == "1" ? true : false;
        //        String prmAction = Request["hidAction"];


        //        //Set file details.
        //        string fileExtension = Path.GetExtension(fl).ToLower();
        //        string filepath = Path.Combine(Server.MapPath("~/materialimages/"), fl);

        //        //Delete if file already exists!
        //        if (System.IO.File.Exists(filepath))
        //            System.IO.File.Delete(filepath);

        //        files.SaveAs(filepath);

        //        if (fileExtension.CompareTo(".xls") == 0 || fileExtension.CompareTo(".xlsx") == 0)
        //        {
        //            files.InputStream.Dispose();
        //        }

        //        MaterialLogic st = new MaterialLogic();
        //        DataTable dt = st.MaterialCreationMaster(prmMaterialCreationId, prmMaterialGroupId, prmMaterialName,
        //            prmUnitId, prmCompanyCode, filepath, prmReOrderLevel, prmOpeningStock,prmRemarks, prmActive, prmAction, out result);

        //        if (result.IndexOf("succesfully") > -1)
        //            return RedirectToAction("list");
        //        else
        //            return RedirectToAction(prmMaterialCreationId == 0 ? "Index" : "Edit");
        //    }
        //    return RedirectToAction("Index");
        //}
        [SessionExpire]
        public JsonResult SaveResult()
        {
            try
            {
                #region FormData
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
                string prmPhotoPath = Request["photoMaterial"];
                #endregion

                #region FileUpload
                if (Request.Files.Count > 0)
                {
                    HttpFileCollectionBase hfc = Request.Files;
                    // LOOP THROUGH EACH SELECTED FILE.
                    for (int i = 0; i <= hfc.Count - 1; i++)
                    {
                        HttpPostedFileBase hpf = hfc[i];
                        // CREATE A FILE ATTACHMENT.
                        string folderpath = Server.MapPath("~/images/material/");
                        hpf.SaveAs(folderpath + hpf.FileName);
                    }
                }
                #endregion

                MaterialLogic st = new MaterialLogic();
                DataTable dt = st.MaterialCreationMaster(prmMaterialCreationId, prmMaterialGroupId, prmMaterialName,
                    prmUnitId, prmCompanyCode, prmPhotoPath, prmReOrderLevel, prmOpeningStock,
                    prmRemarks, prmActive, prmAction, out result);
            }
            catch (Exception ex)
            {

            }
            return Json(result);
        }

        [SessionExpire]
        public ActionResult GetReport(string viewName)
        {
            DataTable dt = new MasterDataLogic().GetListofAllAddedMasterData("[Admin].[Master_MaterialCreation]", 1, 0);
            if (dt != null && dt.Rows.Count > 0)
                return PartialView(viewName, dt);
            else
                return PartialView(viewName, null);
        }
    }
}