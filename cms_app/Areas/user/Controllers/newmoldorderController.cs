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
    public class newmoldorderController : baseClass
    {
        string result = string.Empty;

        // GET: user/newmoldorder
        public ActionResult Index(string prmId)
        {
            ViewBag.MoldId = (!string.IsNullOrEmpty(prmId)) ? prmId : string.Empty;
            DataSet ds = new MasterDataLogic().GetListofAllAddedMasterDataForEdit(prmId, "[Admin].[Master_Mold]");
            ViewData["dsData"] =ds;
            return View();
        }

        // GET: user/newmoldorder
        public ActionResult list()
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
                String prmMoldId = Request["MoldId"];
                String prmContactPerson = Request["contactperson"];
                String prmContactNo = Request["contactpersonmobile"];
                String prmFollowUpPerson = Request["followUpPerson"];
                String prmMoldMaker = Request["moldMaker"];
                String prmSize = Request["size"];
                String prmGSTNo = Request["gstno"];
                String prmGradientRequirement = Request["gradientRequirement"];
                String prmShippingInstruction = Request["shippingInstruction"];
                String prmAddress = Request["address"];
                String prmRemarks = Request["remarks"];
                String prmOrderDate = Request["orderDate"];
                String prmLastSubmitionDate = Request["lastSubmitionDate"];
                #endregion

                MoldLogic st = new MoldLogic();
                DataTable dt = st.NewMoldOrder(
                    prmMoldId, prmContactPerson, prmContactNo, prmFollowUpPerson, prmMoldMaker, prmSize, prmGSTNo, 
                    prmGradientRequirement, prmShippingInstruction, prmAddress, prmRemarks, prmOrderDate, prmLastSubmitionDate,out result);

            }
            catch (Exception ex)
            {
                result = ex.ToString();
            }
            return Json(result);
        }

        [SessionExpire]
        public ActionResult GetReport(string viewName)
        {
            DataTable dt = new MasterDataLogic().GetListofAllAddedMasterData("ForMoldOrder", 1, 0);
            if (dt != null && dt.Rows.Count > 0)
                return PartialView(viewName, dt);
            else
                return PartialView(viewName, null);
        }
    }
}