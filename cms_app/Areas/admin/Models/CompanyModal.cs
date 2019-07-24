using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace cms_app.Areas.admin.Models
{
    public class CompanyModal
    {
        public DataTable CompanyManage(
            String prmCompanyCode, String prmCompanyName, String prmAlias, String prmStateId, String prmCityId, String prmRegAddress,
            String prmBankName, String prmBranchName, String prmBankAcctNo, String prmPANNo, String prmMICRNo, String prmGSTNo,
            String prmContactEmail, String prmPhone, String prmFax, String prmWebsite, String prmContactPerson, String prmMobileNo,
            bool prmActive, String prmAction, out string strMsg
        )
        {
            string uid = "1";
            if (HttpContext.Current.Session["userInfo"] != null)
            {
                cms_app.Models.LoginModalData dm = (cms_app.Models.LoginModalData)HttpContext.Current.Session["userInfo"];
                uid = dm.userId;
            }
             string query = "<Data><Company CompanyCode=\"" + prmCompanyCode + "\" CompanyName=\"" + prmCompanyName + "\" Alias=\"" + prmAlias + "\" StateId=\"" + prmStateId + "\" CityId=\"" + prmCityId + "\" RegAddress1=\"" + prmRegAddress + "\" BankName=\"" + prmBankName + "\" BranchName=\"" + prmBranchName + "\" BankAcctNo=\"" + prmBankAcctNo +
                           "\" PANNo=\"" + prmPANNo + "\" MICRNo=\"" + prmMICRNo + "\" GSTNo=\"" + prmGSTNo + "\" ContactEmail=\"" + prmContactEmail + "\" Phone=\"" + prmPhone + "\" Fax=\"" + prmFax + "\" Website =\"" + prmWebsite + "\" ContactPerson =\"" + prmContactPerson + "\" MobileNo =\"" + prmMobileNo + "\" Active=\"" + prmActive + "\" Action=\"" + prmAction + "\" CreatedBy=\"" + uid + "\"></Company></Data>";

            DataTable Dt = new DataTable();
            ExecuteOperation op = new ExecuteOperation();
            Dt = op.ManageData(query, "[Admin].[Master_CompanyManage]", out strMsg);
            return Dt;
        }
    }
}