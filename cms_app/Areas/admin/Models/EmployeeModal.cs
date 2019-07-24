using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace cms_app.Areas.admin.Models
{
    public class EmployeeModal
    {
        public DataSet GetAllMasterDataByCompanyId(string prmCompanyCode, string prmMDHCode)
        {
            DataSet Ds = new DataSet();
            ExecuteOperation op = new ExecuteOperation();
            Ds = op.GetAllMasterDataByCompanyId(prmCompanyCode, prmMDHCode);
            return Ds;
        }

        public DataTable EmployeeManage(
            String prmEmpId, String prmCompanyCode, String prmRole, String prmEmpCode, String prmEmpPass, String prmEmailId, String prmPhone,
            String prmDOJ, String prmDept, String prmEmpName, String prmFatherName, String prmMaritalStatus, String prmBlood, String prmAddress,
            String prmPANNo, String prmAadharNo, String prmDOB, String prmRemark, bool prmActive, String prmAction, out string strMsg
        )
        {
            string uid = "1";
            if (HttpContext.Current.Session["userInfo"] != null)
            {
                cms_app.Models.LoginModalData dm = (cms_app.Models.LoginModalData)HttpContext.Current.Session["userInfo"];
                uid = dm.userId;
            }

            string query = "<Data><Employee EmployeeId=\"" + prmEmpId + "\" CompanyCode=\"" + prmCompanyCode + "\" Role=\"" + prmRole + "\" EmpCode=\"" + prmEmpCode +
                           "\" EmpPass=\"" + prmEmpPass + "\" EmailId=\"" + prmEmailId + "\" Phone=\"" + prmPhone + "\" DOJ=\"" + prmDOJ + "\" Department=\"" + prmDept +
                           "\" EmpName=\"" + prmEmpName + "\" FatherName=\"" + prmFatherName + "\" MaritalStatus=\"" + prmMaritalStatus + "\" Blood =\"" + prmBlood +
                           "\" Address=\"" + prmAddress + "\" PANNo=\"" + prmPANNo + "\" AadharNo=\"" + prmAadharNo + "\" DOB=\"" + prmDOB + "\" Remark=\"" + prmRemark +
                           "\" Active=\"" + prmActive + "\" Action=\"" + prmAction + "\" CreatedBy=\"" + uid + "\"></Employee></Data>";

            DataTable Dt = new DataTable();
            ExecuteOperation op = new ExecuteOperation();
            Dt = op.ManageData(query, "[Admin].[Master_EmployeeManage]", out strMsg);
            return Dt;
        }
    }
}