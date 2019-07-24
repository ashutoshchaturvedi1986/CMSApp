using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace cms_app.Areas.admin.Models
{
    public class DeptLogic
    {
        public DataTable DepartmentManage(String prmDeptId, String prmDeptCode, String prmDepartment, String prmCompanyCode, String prmDeptHead, String prmRemark, bool prmActive, String prmAction, out string strMsg)
        {
            string uid = "1";
            if (HttpContext.Current.Session["userInfo"] != null)
            {
                cms_app.Models.LoginModalData dm = (cms_app.Models.LoginModalData)HttpContext.Current.Session["userInfo"];
                uid = dm.userId;
            }

            string query = "<Data><Department DeptId=\"" + prmDeptId + "\" DeptCode=\"" + prmDeptCode + "\" Department=\"" + prmDepartment + "\" CompanyCode=\"" + prmCompanyCode + "\" DepartmentHead=\"" + prmDeptHead +
                "\" Remarks=\"" + prmRemark + "\" Active=\"" + prmActive + "\" Action=\"" + prmAction + "\" CreatedBy=\"" + uid + "\"></Department></Data>";

            DataTable Dt = new DataTable();
            ExecuteOperation op = new ExecuteOperation();
            Dt = op.ManageData(query, "[Admin].[Master_DepartmentManage]", out strMsg);
            return Dt;
        }

        //public DataSet DepartmentManage(String prmDeptId, String prmDeptCode, String prmDepartment, String prmCompanyCode, String prmDeptHead, String prmRemark, bool prmActive, String prmAction, out string strMsg)
        //{
        //    string uid = "1";
        //    if (HttpContext.Current.Session["userInfo"] != null)
        //    {
        //        cms_app.Models.LoginModalData dm = (cms_app.Models.LoginModalData)HttpContext.Current.Session["userInfo"];
        //        uid = dm.userId;
        //    }

        //    string query = "<Data><Department DeptId=\"" + prmDeptId + "\" DeptCode=\"" + prmDeptCode + "\" Department=\"" + prmDepartment + "\" CompanyCode=\"" + prmCompanyCode + "\" DepartmentHead=\"" + prmDeptHead +
        //        "\" Remarks=\"" + prmRemark + "\" Active=\"" + prmActive + "\" Action=\"" + prmAction + "\" CreatedBy=\"" + uid + "\"></Department></Data>";

        //    ExecuteOperation op = new ExecuteOperation();
        //    DataSet ds = op.GetDataSetFromInputParameterWithMessage(query, "[Admin].[Master_DepartmentManage]", out strMsg);
        //    return ds;
        //}
    }
}