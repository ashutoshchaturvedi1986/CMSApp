using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using cms_app.Areas.admin.Models;
using System.Data;
using Newtonsoft.Json;
using cms_app.Models.Common;

namespace cms_app.Areas.admin.Controllers
{
    public class materialgroupController : baseClass
    {
        string result = string.Empty;

        // GET: admin/materialgroup
        public ActionResult Index()
        {
            DataSet ds = new MasterDataLogic().GetDataWithCompanyForDropdown(0, "[Admin].[Master_MaterialGroup]");
            if (ds != null && ds.Tables.Count > 0)
            {
                ViewData["dtCompany"] = ds.Tables[0];
                if (ds.Tables.Count >= 1 && ds.Tables[1].Rows.Count > 0)
                    ViewData["dtMaterialGroup"] = ds.Tables[1].Select("MaterialGroupUnder=1").CopyToDataTable();
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
                ds = new MasterDataLogic().GetListofAllAddedMasterDataForEdit(materialId, "[Admin].[Master_MaterialGroup]");
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

        [SessionExpire]
        public JsonResult SaveResult(Int32 prmMaterialGroupId, String prmMaterialGroupName, Int32 prmMaterialGroupUnder, String prmCompanyCode, String prmRemarks, bool prmActive, String prmAction)
        {
            DataTable dt = new MaterialLogic().MaterialGroupManage(prmMaterialGroupId, prmMaterialGroupName, prmMaterialGroupUnder, prmCompanyCode, prmRemarks, prmActive, prmAction, out result);
            return Json(result);
        }

        [SessionExpire]
        public ActionResult GetReport(string viewName)
        {
            DataTable dt = new MasterDataLogic().GetListofAllAddedMasterData("[Admin].[Master_MaterialGroup]", 1, 0);
            if (dt != null && dt.Rows.Count > 0)
                return PartialView(viewName, dt);
            else
                return PartialView(viewName, null);
        }
    }
}