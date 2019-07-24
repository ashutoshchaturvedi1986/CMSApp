using System;
using System.Web.Mvc;
using cms_app.Areas.admin.Models;
using System.Data;
using Newtonsoft.Json;
using cms_app.Models.Common;

namespace cms_app.Areas.admin.Controllers
{
    public class employeeController : baseClass
    {
        string result = string.Empty;

        // GET: admin/employee
        [SessionExpire]
        public ActionResult Index()
        {
            DataSet ds = new MasterDataLogic().GetDataWithCompanyForDropdown(0, "[HR].[Employee]");
            if (ds != null && ds.Tables.Count > 0)
            {
                //ViewData["dtCompany"] = ds.Tables[0];
                //ViewData["dtRole"] = ds.Tables.Count > 1 ? ds.Tables[1]:null;
                ViewData["dsData"] = ds;
            }
            return View();
        }

        // EDIT: admin/state
        [SessionExpire]
        public ActionResult Edit()
        {
            string empId = Request.Form["code"];
            DataSet ds = null;
            if (!string.IsNullOrEmpty(empId))
                ds = new MasterDataLogic().GetListofAllAddedMasterDataForEdit(empId, "[HR].[Employee]");
            ViewData["dsData"] = ds;
            return View();
        }
        //public ActionResult Edit()
        //{
        //    string empId = Request.Form["code"];
        //    if (!string.IsNullOrEmpty(empId))
        //    {
        //        ViewData["dtCompany"] = new MasterDataLogic().GetMasterList("[Admin].[Company]", 1, 0);
        //        MasterDataLogic st = new MasterDataLogic();
        //        DataTable dt = st.GetMasterListForEdit(empId, "[HR].[Employee]");
        //        if (dt != null && dt.Rows.Count > 0)
        //            return View(dt);
        //        else
        //            return View("List");
        //    }
        //    else
        //        return View("List");
        //}

        // GET: admin/employee
        [SessionExpire]
        public ActionResult List()
        {
            return View();
        }

        [SessionExpire]
        public JsonResult SaveResult(
            String prmEmpId, String prmCompanyCode, String prmRole, String prmEmpCode, String prmEmpPass, String prmEmailId, String prmPhone,
            String prmDOJ, String prmDept, String prmEmpName, String prmFatherName, String prmMaritalStatus, String prmBlood, String prmAddress,
            String prmPANNo, String prmAadharNo, String prmDOB, String prmRemark, bool prmActive, String prmAction
        )
        {
            EmployeeModal st = new EmployeeModal();
            DataTable dt = st.EmployeeManage(
                prmEmpId, prmCompanyCode, prmRole, prmEmpCode, prmEmpPass, prmEmailId, prmPhone, prmDOJ, prmDept, prmEmpName, prmFatherName, prmMaritalStatus,
                prmBlood, prmAddress, prmPANNo, prmAadharNo, prmDOB, prmRemark, prmActive, prmAction, out result);

            return Json(result);
        }

        [SessionExpire]
        public ActionResult GetReport(string viewName)
        {
            DataTable dt = new MasterDataLogic().GetListofAllAddedMasterData("[HR].[Employee]", 1, 0);
            if (dt != null && dt.Rows.Count > 0)
                return PartialView(viewName, dt);
            else
                return PartialView(viewName, null);
        }

        [SessionExpire]
        public string FillHierarchy(string prmCompanyCode)
        {
            string JSONresult = string.Empty;
            DataSet dsHierarchy = new DataSet();
            EmployeeModal emp = new EmployeeModal();
            dsHierarchy = emp.GetAllMasterDataByCompanyId(prmCompanyCode, "");
            JSONresult= JsonConvert.SerializeObject(dsHierarchy);

            return JSONresult;
        }
    }
}