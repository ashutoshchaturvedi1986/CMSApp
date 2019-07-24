using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace cms_app.Areas.user.Models
{
    public class OrderLogic
    {
        public DataTable NewOrderManage(
            String prmOrderId, String prmArticleId , String prmCustomerId, String prmSalePersonId, String prmContactPerson, String prmContactNo, String prmAddress, 
            String prmOrderDetail, String prmOrderDate, String prmOrderExpDate, String prmRemark, String prmAction, out string strMsg
        )
        {
            string uid = "1";
            if (HttpContext.Current.Session["userInfo"] != null)
            {
                cms_app.Models.LoginModalData dm = (cms_app.Models.LoginModalData)HttpContext.Current.Session["userInfo"];
                uid = dm.userId;
            }

            string query = "<Data><Order OrderId =\"" + prmOrderId + "\" ArticleId=\"" + prmArticleId + "\" CustomerId =\"" + prmCustomerId + "\" SalePersonId=\"" + prmSalePersonId +
                           "\" ContactPerson=\"" + prmContactPerson + "\" ContactNo=\"" + prmContactNo + "\" Address=\"" + prmAddress + 
                           "\" OrderDetails=\"" + prmOrderDetail + "\" OrderDate=\"" + prmOrderDate + "\" prmOrderExpDate=\"" + prmOrderExpDate + 
                            "\" Remarks=\"" + prmRemark + "\" Action=\"" + prmAction + "\" CreatedBy=\"" + uid + "\"></Order></Data>";
            return new ExecuteOperation().ManageData(query, "[Admin].[Manage_OrderData]", out strMsg);
        }
    }
}