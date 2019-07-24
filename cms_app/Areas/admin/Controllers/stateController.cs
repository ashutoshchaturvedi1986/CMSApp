using cms_app.Areas.admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Web.UI.WebControls;
using cms_app.Models.Common;

namespace cms_app.Areas.admin.Controllers
{
    public class stateController : baseClass
    {
        string result = string.Empty;

        // GET: admin/state
        [SessionExpire]
        public ActionResult Index()
        {
            string strState = string.Empty;
            StateLogic st = new StateLogic();
            DataTable dt = st.StateManage("", "", "", true, "SELECT", out result);
            if (dt != null && dt.Rows.Count > 0)
                strState = string.Join(",", dt.Rows.OfType<DataRow>().Select(r => r["StateId"].ToString()));
            ViewBag.DefaultStates = strState;
            return View();
        }

        [SessionExpire]
        // EDIT: admin/state
        public ActionResult Edit()
        {
            string stateId = Request.Form["code"];
            DataTable dt = null;
            if (!string.IsNullOrEmpty(stateId))
            {
                DataSet ds = new MasterDataLogic().GetListofAllAddedMasterDataForEdit(stateId, "[Admin].[Master_State]");
                if (ds != null && ds.Tables.Count > 0)
                    dt = ds.Tables[0];
            }
            return View(dt);
        }

        [SessionExpire]
        // Get: admin/state
        public ActionResult List()
        {
            return View();
        }

        [SessionExpire]
        public JsonResult SaveResult(String prmStateId, String prmName, String prmRemarks, bool prmActive, String prmAction)
        {
            StateLogic st = new StateLogic();
            DataTable dt = st.StateManage(prmStateId, prmName, prmRemarks, prmActive, prmAction, out result);
            return Json(result);
        }

        [SessionExpire]
        public ActionResult GetReport(string viewName)
        {
            DataTable dt = new MasterDataLogic().GetListofAllAddedMasterData("[Admin].[Master_State]", 1, 0);
            if (dt != null && dt.Rows.Count > 0)
                return PartialView(viewName, dt);
            else
                return PartialView(viewName, null);
        }
    }
}