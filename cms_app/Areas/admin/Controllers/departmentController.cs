using System;
using System.Web.Mvc;
using cms_app.Areas.admin.Models;
using System.Data;
using Newtonsoft.Json;
using cms_app.Models.Common;

namespace cms_app.Areas.admin.Controllers
{
    public class departmentController : baseClass
    {
        string result = string.Empty;

        // GET: admin/employee
        [SessionExpire]
        public ActionResult Index()
        {
            //ViewData["dtCompany"] = new MasterDataLogic().GetMasterList("[Admin].[Company]", 1, 0);
            DataSet ds = new MasterDataLogic().GetDataWithCompanyForDropdown(0, "[Admin].[Department]");
            if (ds != null && ds.Tables.Count>0)
            {
                ViewData["dtCompany"] = ds.Tables[0];
                ViewData["dtEmployee"] = ds.Tables[1];
            }
            return View();
        }

        // EDIT: admin/state
        [SessionExpire]
        public ActionResult Edit()
        {
            string depId = Request.Form["code"];
            DataSet ds = null;
            if (!string.IsNullOrEmpty(depId))
            {
                ds = new MasterDataLogic().GetListofAllAddedMasterDataForEdit(depId, "[Admin].[Department]");
            }
            ViewData["dsData"] = ds;
            return View();
        }

        // GET: admin/employee
        [SessionExpire]
        public ActionResult List()
        {
            return View();
        }

        [SessionExpire]
        public JsonResult SaveResult(String prmDeptId,String prmDeptCode, String prmDepartment, String prmCompanyCode, String prmDepartmentHead, String prmRemark, bool prmActive, String prmAction)
        {
            DataTable dt = new DeptLogic().DepartmentManage(prmDeptId,prmDeptCode, prmDepartment, prmCompanyCode, prmDepartmentHead, prmRemark, prmActive, prmAction, out result);
            return Json(result);
        }

        [SessionExpire]
        public ActionResult GetReport(string viewName)
        {
            DataTable dt = new MasterDataLogic().GetListofAllAddedMasterData("[Admin].[Department]", 1, 0);
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
            dsHierarchy = emp.GetAllMasterDataByCompanyId(prmCompanyCode,"");
            JSONresult = JsonConvert.SerializeObject(dsHierarchy);

            return JSONresult;
        }
    }
}