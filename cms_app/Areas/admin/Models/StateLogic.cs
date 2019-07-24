using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace cms_app.Areas.admin.Models
{
    public class StateLogic
    {
        public DataTable StateManage(String prmStateId, String prmName, String prmRemarks, bool prmActive, String prmAction,out string strMsg)
        {
            string uid = "1";
            if (HttpContext.Current.Session["userInfo"] != null)
            {
                cms_app.Models.LoginModalData dm = (cms_app.Models.LoginModalData)HttpContext.Current.Session["userInfo"];
                uid = dm.userId;
            }
            string query = "<Data><State StateId=\"" + prmStateId + "\" Name=\"" + prmName + "\" Remarks=\"" + prmRemarks +
                "\" Active=\"" + prmActive + "\" Action=\"" + prmAction + "\" CreatedBy=\"" + uid + "\"></State></Data>";
            //strMsg = string.Empty;

            DataTable Dt = new DataTable();
            ExecuteOperation op = new ExecuteOperation();
            Dt = op.ManageData(query, "[Admin].[Master_StateManage]", out strMsg);
            return Dt;
        }
    }
}