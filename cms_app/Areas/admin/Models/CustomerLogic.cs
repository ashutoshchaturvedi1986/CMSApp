using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace cms_app.Areas.admin.Models
{
    public class CustomerLogic
    {
        public DataTable CustomerManage(String prmCustomerId, String prmCustomerCode, String prmCustomerName, String prmCompanyCode, String prmAddress,
            String prmGSTNo, String prmPANNo, String prmBillingAddress, String prmContactPerson,String prmContactNo,String prmWebsite, String prmEmailID, 
            String prmRemark, bool prmActive, String prmAction, out string strMsg)
        {
            string uid = "1";
            if (HttpContext.Current.Session["userInfo"] != null)
            {
                cms_app.Models.LoginModalData dm = (cms_app.Models.LoginModalData)HttpContext.Current.Session["userInfo"];
                uid = dm.userId;
            }

            string query = "<Data><Customer CustomerId=\"" + prmCustomerId + "\" CustomerCode=\"" + prmCustomerCode + "\" CustomerName=\"" + prmCustomerName + "\" CompanyCode=\"" + prmCompanyCode + "\" Address=\"" + prmAddress +
                           "\" GSTNo=\"" + prmGSTNo + "\" PANNo=\"" + prmPANNo + "\" BillingAddress=\"" + prmBillingAddress + "\" ContactPerson=\"" + prmContactPerson +
                           "\" ContactNo=\"" + prmContactNo + "\" Website=\"" + prmWebsite + "\" EmailID=\"" + prmEmailID +
                           "\" Remarks=\"" + prmRemark + "\" Active=\"" + prmActive + "\" Action=\"" + prmAction + "\" CreatedBy=\"" + uid + "\"></Customer></Data>";

            DataTable Dt = new DataTable();
            ExecuteOperation op = new ExecuteOperation();
            Dt = op.ManageData(query, "[Admin].[Master_CustomerManage]", out strMsg);
            return Dt;
        }
    }
}