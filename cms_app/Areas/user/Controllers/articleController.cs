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
    public class articleController : baseClass
    {
        string result = string.Empty;

        // GET: user/article
        [SessionExpire]
        public ActionResult Index(string code)
        {
            if (!String.IsNullOrEmpty(code))
            {
                DataSet ds = new ArticleLogic().GetDataForCreateArticle(code);
                if (ds != null && ds.Tables.Count > 0)
                {
                    ViewData["dsData"] = ds;
                }
                //ViewBag.MoldId = code;
            }
            return View();
        }

        [SessionExpire]
        public ActionResult articlerowmaterial(string code)
        {
            if (!String.IsNullOrEmpty(code))
            {
                DataSet ds = new ArticleLogic().GetDataForUpdateArticle(code);
                if (ds != null && ds.Tables.Count > 0)
                {
                    ViewData["dsData"] = ds;
                }
            }
            return View();
        }


        [SessionExpire]
        public ActionResult List()
        {
            return View();
        }

        [SessionExpire]
        public ActionResult articlelist()
        {
            return View();
        }

        [SessionExpire]
        public ActionResult GetReport(string action, string viewName)
        {
            //ArticleLogic st = new ArticleLogic();
            //DataTable dt = st.GetArticleList(action, "[Admin].[Article_GetList]");
            DataTable dt = new MasterDataLogic().GetListofAllAddedMasterData(action, 1, 0);
            if (dt != null && dt.Rows.Count > 0)
                return PartialView(viewName, dt);
            else
                return PartialView(viewName, null);
        }

        [SessionExpire, ValidateInput(false)]
        public JsonResult SaveResult()
        {
            try
            {
                #region FormData
                /*Parameters of Query Strings*/
                String prmMoldId = Request["hidId"];
                String prmBrand = Request["brand"];
                String prmColor = Request["color"];
                String prmProcess = Request["process"];
                String prmSubProcess = Request["subprocess"];
                String prmSize = Request["size"];
                String prmArticleDate = Request["articleDate"];
                String prmIsBeforeProcess = Request["isBeforeProcess"];
                String prmIsAfterProcess = Request["isAfterProcess"];
                String prmLang = Request["lang"];
                String prmDescription = Request["description"];
                String prmSubProcessInputs = Request["subProcessInputs"];
                #endregion

                ArticleLogic st = new ArticleLogic();
                DataTable dt = st.SaveArticle(prmMoldId, prmBrand, prmColor, prmProcess, prmSubProcess, prmSize, prmArticleDate, prmIsBeforeProcess, prmIsAfterProcess, prmLang, prmDescription, true, "INSERT", prmSubProcessInputs, out result);
            }
            catch (Exception ex)
            {
                result = ex.ToString();
            }
            return Json(result);
        }

        [SessionExpire, ValidateInput(false)]
        public JsonResult SaveRawMaterialResult()
        {
            try
            {
                #region FormData
                /*Parameters of Query Strings*/
                String prmSubProcessInputs = Request["subProcessInputs"];
                #endregion

                ArticleLogic st = new ArticleLogic();
                DataTable dt = st.SaveArticleRawMaterialInfo(prmSubProcessInputs, out result);
            }
            catch (Exception ex)
            {
                result = ex.ToString();
            }
            return Json(result);
        }
    }
}