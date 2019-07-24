using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace cms_app.Areas.admin.Models
{
    public class UnitLogic
    {
        public DataTable UnitManage(Int32 prmUnitId, String prmUnitCode, String prmName, String prmCompanyCode, Int32 prmDecimal, String prmRemark, bool prmActive, String prmAction, out string strMsg)
        {
            string uid = "1";
            if (HttpContext.Current.Session["userInfo"] != null)
            {
                cms_app.Models.LoginModalData dm = (cms_app.Models.LoginModalData)HttpContext.Current.Session["userInfo"];
                uid = dm.userId;
            }

            string query = "<Data><Unit UnitId=\"" + prmUnitId + "\" UnitCode=\"" + prmUnitCode + "\" UnitName=\"" + prmName + "\" CompanyCode=\"" + prmCompanyCode + "\" NoofDecimal=\"" + prmDecimal +
                "\" Remarks=\"" + prmRemark + "\" Active=\"" + prmActive + "\" Action=\"" + prmAction + "\" CreatedBy=\"" + uid + "\"></Unit></Data>";

            DataTable Dt = new DataTable();
            ExecuteOperation op = new ExecuteOperation();
            Dt = op.ManageData(query, "[Admin].[Master_UnitManage]", out strMsg);
            return Dt;
        }
    }
}