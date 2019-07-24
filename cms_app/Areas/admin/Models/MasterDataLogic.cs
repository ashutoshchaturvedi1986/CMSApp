using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace cms_app.Areas.admin.Models
{
    public class MasterDataLogic
    {
        public DataTable GetMasterListForEdit(String prmId,String prmTableName)
        {
            DataTable Dt = new DataTable();
            ExecuteOperation op = new ExecuteOperation();
            Dt = op.GetMasterListForEdit(prmId,prmTableName);
            return Dt;
        }

        public DataTable GetMasterList(String prmTableName, Int32 prmActive, Int32 prmIsDelete)
        {
            DataTable Dt = new DataTable();
            ExecuteOperation op = new ExecuteOperation();
            Dt = op.GetMasterDataList(prmTableName, prmActive, prmIsDelete);
            return Dt;
        }

        public DataSet GetDataWithCompanyForDropdown(Int32 SelectedId, String prmTableName)
        {
            DataSet DS = new DataSet();
            ExecuteOperation op = new ExecuteOperation();
            DS = op.GetDataWithCompanyForDropdown(SelectedId,prmTableName);
            return DS;
        }

        public DataTable MasterDataByCategory(String prmCompanyCode, String prmMDDCode, String prmParrentMDDCode, String prmMDHCode,String prmMDDName, 
            String prmRemarks, int prmActive, int prmIsDelete, String prmAction, out string strMsg)
        {
            string uid = "1";
            if (HttpContext.Current.Session["userInfo"] != null)
            {
                cms_app.Models.LoginModalData dm = (cms_app.Models.LoginModalData)HttpContext.Current.Session["userInfo"];
                uid = dm.userId;
            }
            string query = "<Data><Category CompanyCode=\"" + prmCompanyCode + "\" MDDCode=\"" + prmMDDCode + "\" MDHCode =\"" + prmMDHCode + 
                           "\" MDDName=\"" + prmMDDName + "\" ParrentMDDCode=\"" + prmParrentMDDCode + "\" Remarks=\"" + prmRemarks +"\" Active=\"" + prmActive + "\" isDelete=\"" + prmIsDelete + 
                           "\" Action=\"" + prmAction + "\" CreatedBy=\"" + uid + "\"></Category></Data>";
            DataTable Dt = new DataTable();
            ExecuteOperation op = new ExecuteOperation();
            Dt = op.ManageData(query, "[Admin].[Master_ListByCategory]", out strMsg);
            return Dt;
        }


        // Newly Added Function
        public DataTable GetListofAllAddedMasterData(String prmTableName, Int32 prmActive, Int32 prmIsDelete)
        {
            DataTable Dt = new DataTable();
            ExecuteOperation op = new ExecuteOperation();
            Dt = op.GetListofAllAddedMasterData(prmTableName, prmActive, prmIsDelete);
            return Dt;
        }

        public DataSet GetListofAllAddedMasterDataForEdit(String prmId, String prmTableName)
        {
            DataSet Ds = new DataSet();
            ExecuteOperation op = new ExecuteOperation();
            Ds = op.GetListofAllAddedMasterDataForEdit(prmId,prmTableName);
            return Ds;
        }

        public DataSet MasterDataByCategory1(String prmCompanyCode, String prmMDDCode, String prmParrentMDDCode, String prmMDHCode, String prmMDDName,
            String prmRemarks, int prmActive, int prmIsDelete, String prmAction, out string strMsg)
        {
            DataSet DS = new DataSet();
            ExecuteOperation op = new ExecuteOperation();
            DS = op.MasterDataByCategory(prmCompanyCode, prmMDDCode, prmParrentMDDCode, prmMDHCode, prmMDDName, prmRemarks, prmActive, prmIsDelete, prmAction, out strMsg);
            return DS;
        }
    }
}