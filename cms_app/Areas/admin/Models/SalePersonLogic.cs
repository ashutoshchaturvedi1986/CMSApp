using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace cms_app.Areas.admin.Models
{
    public class SalePersonLogic
    {
        public DataTable SalePersonManage(String prmSalePersonId, String prmSalePersonCode, String prmSalePersonName, String prmCompanyCode, String prmContactNo, String prmAddress,
            String prmDOB, String prmPanNo, String prmAdharNo, String prmDOJ, String prmRemark, bool prmActive, String prmAction, out string strMsg)
        {
            string uid = "1";
            if (HttpContext.Current.Session["userInfo"] != null)
            {
                cms_app.Models.LoginModalData dm = (cms_app.Models.LoginModalData)HttpContext.Current.Session["userInfo"];
                uid = dm.userId;
            }

            string query = "<Data><SalePerson SalePersonId=\"" + prmSalePersonId + "\" SalePersonCode=\"" + prmSalePersonCode + "\" SalePersonName=\"" + prmSalePersonName + 
                "\" CompanyCode=\"" + prmCompanyCode + "\" ContactNo=\"" + prmContactNo+ "\" Address=\"" + prmAddress +
                           "\" DOB=\"" + prmDOB + "\" PanNo=\"" + prmPanNo + "\" AdharNo=\"" + prmAdharNo + "\" DOJ=\"" + prmDOJ +
                           "\" Remarks=\"" + prmRemark + "\" Active=\"" + prmActive + "\" Action=\"" + prmAction + "\" CreatedBy=\"" + uid + "\"></SalePerson></Data>";

            DataTable Dt = new DataTable();
            ExecuteOperation op = new ExecuteOperation();
            Dt = op.ManageData(query, "[Admin].[Master_SalePersonManage]", out strMsg);
            return Dt;
        }
    }
}