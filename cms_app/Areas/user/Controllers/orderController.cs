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
    public class orderController : baseClass
    {
        string result = string.Empty;

        // GET: user/article
        [SessionExpire]
        public ActionResult Index(string code)
        {
            if (!String.IsNullOrEmpty(code))
            {
                Int32 id = Int32.TryParse(code, out id) ? id : 0;
                DataSet ds = new MasterDataLogic().GetDataWithCompanyForDropdown(id, "OrderCreation");
                if (ds != null && ds.Tables.Count > 0)
                {
                    ViewData["dsData"] = ds;
                }

                //if (System.Web.HttpContext.Current.Session["userInfo"] != null)
                //{
                //    cms_app.Models.LoginModalData dm = (cms_app.Models.LoginModalData)System.Web.HttpContext.Current.Session["userInfo"];
                //    ViewData["userId"] = dm.userId;
                //}
            }
            return View();
        }


        [SessionExpire]
        public ActionResult confirmation()
        {
            return View();
        }

        [SessionExpire]
        public ActionResult List()
        {
            return View();
        }

        [SessionExpire]
        public ActionResult GetReport(string action, string viewName)
        {
            DataTable dt = new MasterDataLogic().GetListofAllAddedMasterData(action, 1, 0);
            if (dt != null && dt.Rows.Count > 0)
                return PartialView(viewName, dt);
            else
                return PartialView(viewName, null);
        }

        [SessionExpire]
        [ValidateInput(false)]
        public JsonResult SaveResult()
        {
            try
            {
                #region FormData
                /*Parameters of Query Strings*/
                String prmOrderId = Request["orderId"];
                String prmArticleId = Request["hidId"];
                String prmCustomerId = Request["customer"];
                String prmSalePersonId = Request["salePerson"];
                String prmContactPerson = Request["contactperson"];
                String prmContactNo = Request["contactpersonmobile"];
                String prmAddress = Request["customeraddress"];
                //String prmSize = Request["size"];
                //String prmQuantity = Request["quantity"];
                //String prmRate = Request["rate"];
                //String prmTotalQuantity = Request["totalQuantity"];
                //String prmAverageRate = Request["averageRate"];
                String prmOrderDate = Request["orderDate"];
                String prmOrderExpDate = Request["orderExpDate"];
                String prmRemarks = Request["remarks"];
                String prmAction = Request["action"];

                string xml = Request["inputXml"];
                #endregion

                OrderLogic st = new OrderLogic();
                DataTable dt = st.NewOrderManage(
                    prmOrderId,prmArticleId, prmCustomerId, prmSalePersonId, prmContactPerson, prmContactNo, prmAddress, xml, 
                    prmOrderDate, prmOrderExpDate, prmRemarks, prmAction, out result);
            }
            catch (Exception ex)
            {
                result = ex.ToString();
            }
            return Json(result);
        }
    }
}