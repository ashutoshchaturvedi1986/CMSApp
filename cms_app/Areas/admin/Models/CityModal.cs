using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using  System.Data;

namespace cms_app.Areas.admin.Models
{
    public class CityModal
    {
        public string CityId { get; set; }
        public string StateId { get; set; }
        public string Name { get; set; }
        public string Remarks { get; set; }
        public bool Active { get; set; }
        public bool isDelete { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
    }

    public class CityLogic
    {
        public DataTable CityManage(String prmCityId, String prmStateId, String prmName, String prmRemarks, bool prmActive, String prmAction, out string strMsg)
        {
            string uid = "1";
            if (HttpContext.Current.Session["userInfo"] != null)
            {
                cms_app.Models.LoginModalData dm = (cms_app.Models.LoginModalData)HttpContext.Current.Session["userInfo"];
                uid = dm.userId;
            }
            string query = "<Data><City CityId=\"" + prmCityId + "\" StateId=\"" + prmStateId + "\" Name=\"" + prmName + "\" Remarks=\"" + prmRemarks +
                           "\" Active=\"" + prmActive + "\" Action=\"" + prmAction + "\" CreatedBy=\"" + uid + "\"></City></Data>";

            DataTable Dt = new DataTable();
            ExecuteOperation op = new ExecuteOperation();
            Dt = op.ManageData(query, "[Admin].[Master_CityManage]", out strMsg);
            return Dt;
        }

        public DataTable GetCityByStateId(string prmStateId)
        {
            DataTable Dt = new DataTable();
            ExecuteOperation op = new ExecuteOperation();
            Dt = op.GetCityByStateId(prmStateId);
            return Dt;
        }
    }
}