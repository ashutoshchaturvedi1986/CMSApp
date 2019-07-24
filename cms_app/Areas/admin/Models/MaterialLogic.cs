using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace cms_app.Areas.admin.Models
{
    public class MaterialLogic
    {
        public DataTable MaterialGroupManage(Int32 prmMaterialGroupId, String prmMaterialGroupName, Int32 prmMaterialGroupUnder, String prmCompanyCode, String prmRemarks, bool prmActive, String prmAction, out string strMsg)
        {
            string uid = "1";
            if (HttpContext.Current.Session["userInfo"] != null)
            {
                cms_app.Models.LoginModalData dm = (cms_app.Models.LoginModalData)HttpContext.Current.Session["userInfo"];
                uid = dm.userId;
            }
            string query = "<Data><MaterialGroup MaterialGroupId=\"" + prmMaterialGroupId + "\" MaterialGroupName=\"" + prmMaterialGroupName +
                           "\" MaterialGroupUnder=\"" + prmMaterialGroupUnder + "\" CompanyCode=\"" + prmCompanyCode + "\" Remarks=\"" + prmRemarks +
                           "\" Active=\"" + prmActive + "\" Action=\"" + prmAction + "\" CreatedBy=\"" + uid + "\"></MaterialGroup></Data>";

            DataTable Dt = new DataTable();
            ExecuteOperation op = new ExecuteOperation();
            Dt = op.ManageData(query, "[Admin].[Master_MaterialGroupManage]", out strMsg);
            return Dt;
        }

        //public DataTable GetCityByStateId(string prmStateId)
        //{
        //    DataTable Dt = new DataTable();
        //    ExecuteOperation op = new ExecuteOperation();
        //    Dt = op.GetCityByStateId(prmStateId);
        //    return Dt;
        //}

        public DataTable MaterialCreationMaster(Int32 prmMaterialCreationId, Int32 prmMaterialGroupId,  String prmMaterialName,
            Int32 prmUnitId, String prmCompanyCode, String prmPhotoPath,String prmReOrderLevel,Decimal prmOpeningStock,
            String prmRemarks, bool prmActive, String prmAction, out string strMsg)
        {
            string uid = "1";
            if (HttpContext.Current.Session["userInfo"] != null)
            {
                cms_app.Models.LoginModalData dm = (cms_app.Models.LoginModalData)HttpContext.Current.Session["userInfo"];
                uid = dm.userId;
            }
            string query = "<Data><MaterialCreation MaterialCreationId=\"" + prmMaterialCreationId + "\" MaterialGroupId=\"" + prmMaterialGroupId + "\" MaterialName=\"" + prmMaterialName +
                           "\" UnitId=\"" + prmUnitId + "\" CompanyCode=\"" + prmCompanyCode + "\" PhotoPath=\"" + prmPhotoPath + 
                           "\" ReOrderLevel=\"" + prmReOrderLevel + "\" OpeningStock=\"" + prmOpeningStock +
                           "\" Remarks=\"" + prmRemarks +
                           "\" Active=\"" + prmActive + "\" Action=\"" + prmAction + "\" CreatedBy=\"" + uid + "\"></MaterialCreation></Data>";

            DataTable Dt = new DataTable();
            ExecuteOperation op = new ExecuteOperation();
            Dt = op.ManageData(query, "[Admin].[Master_MaterialCreationSP]", out strMsg);
            return Dt;
        }
    }
}