using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;


namespace cms_app.Areas.admin.Models
{
    public class SupplierLogic
    {
        public DataTable SupplierManage(String prmSupplierId, String prmSupplierCode, String prmSupplierName, String prmCompanyCode, String prmAddress,
            String prmGSTNo, String prmPANNo, String prmContactPerson, String prmContactNo, String prmWebsite, String prmEmailID,
            String prmRemark, bool prmActive, String prmAction, out string strMsg)
        {
            string uid = "1";
            if (HttpContext.Current.Session["userInfo"] != null)
            {
                cms_app.Models.LoginModalData dm = (cms_app.Models.LoginModalData)HttpContext.Current.Session["userInfo"];
                uid = dm.userId;
            }

            string query = "<Data><Supplier SupplierId=\"" + prmSupplierId + "\" SupplierCode=\"" + prmSupplierCode + "\" SupplierName=\"" + prmSupplierName + "\" CompanyCode=\"" + prmCompanyCode + "\" Address=\"" + prmAddress +
                           "\" GSTNo=\"" + prmGSTNo + "\" PANNo=\"" + prmPANNo + "\" ContactPerson=\"" + prmContactPerson +
                           "\" ContactNo=\"" + prmContactNo + "\" Website=\"" + prmWebsite + "\" EmailID=\"" + prmEmailID +
                           "\" Remarks=\"" + prmRemark + "\" Active=\"" + prmActive + "\" Action=\"" + prmAction + "\" CreatedBy=\"" + uid + "\"></Supplier></Data>";

            DataTable Dt = new DataTable();
            ExecuteOperation op = new ExecuteOperation();
            Dt = op.ManageData(query, "[Admin].[Master_SupplierManage]", out strMsg);
            return Dt;
        }

    }
}