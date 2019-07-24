using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace cms_app.Areas.user.Models
{
    public class MoldLogic
    {
        public DataTable NewMoldManage(
            String prmMoldId, String prmCompanyCode, String prmMoldName, String prmCustomerId, String prmAddress, String prmContactPerson, String prmContactNo,
            String prmTechnology, String prmSize, String prmColor, String prmDesignerId, String prmCourierAddress,
            String prmLastSubmitionDate, String prmAssignDate, String prmMoldPicture, String prmTechPacDetail,
            String prmRemark, bool prmActive, String prmAction, String prmIsOrder, out string strMsg
        )
        {
            string uid = "1";
            if (HttpContext.Current.Session["userInfo"] != null)
            {
                cms_app.Models.LoginModalData dm = (cms_app.Models.LoginModalData)HttpContext.Current.Session["userInfo"];
                uid = dm.userId;
            }

            string query = "<Data><Mold MoldId=\"" + prmMoldId + "\" CompanyCode=\"" + prmCompanyCode + "\" MoldName=\"" + prmMoldName +
                "\" CustomerId =\"" + prmCustomerId + "\" Address=\"" + prmAddress + "\" ContactPerson=\"" + prmContactPerson + "\" ContactNo=\"" + prmContactNo +
               "\" Technology=\"" + prmTechnology + "\" Size=\"" + prmSize + "\" Colour=\"" + prmColor + "\" DesignerId=\"" + prmDesignerId +
               "\" CourierAddress=\"" + prmCourierAddress + "\" LastSubmitionDate=\"" + prmLastSubmitionDate + "\" AssignDate=\"" + prmAssignDate +
               "\" MoldPicture=\"" + prmMoldPicture + "\" TechPacDetail=\"" + prmTechPacDetail +
               "\" Remarks=\"" + prmRemark + "\" Active=\"" + prmActive + "\" Action=\"" + prmAction + "\" IsOrder=\"" + prmIsOrder + "\" CreatedBy=\"" + uid + "\"></Mold></Data>";

            DataTable Dt = new DataTable();
            ExecuteOperation op = new ExecuteOperation();
            //Dt = op.ManageData(query, "[Admin].[Master_MoldManage]", out strMsg);
            Dt = op.ManageData(query, "[Admin].[Mold_SaveMoldData]", out strMsg);
            return Dt;
        }


        public DataTable NewMoldOrder(
            String prmMoldId, String prmContactPerson, String prmContactNo, String prmFollowUpPerson, String prmMoldMaker, 
            String prmSize, String prmGSTNo, String prmGradientRequirement, String prmShippingInstruction, String prmAddress, String prmRemark,
            String prmOrderDate, String prmLastSubmitionDate, out string strMsg
        )
        {
            string uid = "1";
            if (HttpContext.Current.Session["userInfo"] != null)
            {
                cms_app.Models.LoginModalData dm = (cms_app.Models.LoginModalData)HttpContext.Current.Session["userInfo"];
                uid = dm.userId;
            }

            string query = "<Data><MoldOrder MoldId=\"" + prmMoldId + "\" ContactPerson=\"" + prmContactPerson + "\" ContactNo=\"" + prmContactNo +
                "\" FollowUpPerson=\"" + prmFollowUpPerson + "\" MoldMaker=\"" + prmMoldMaker + "\" Size=\"" + prmSize + "\" GSTNo=\"" + prmGSTNo +
                "\" GradientRequirement=\"" + prmGradientRequirement + "\" ShippingInstruction=\"" + prmShippingInstruction +"\" Address=\"" + prmAddress + 
                "\" Remarks=\"" + prmRemark + "\" OrderDate=\"" + prmOrderDate + "\" LastSubmitionDate=\"" + prmLastSubmitionDate + "\" CreatedBy=\"" + uid + 
                "\"></MoldOrder></Data>";

            DataTable Dt = new DataTable();
            ExecuteOperation op = new ExecuteOperation();
            Dt = op.ManageData(query, "[Admin].[Mold_SaveMoldOrderData]", out strMsg);
            return Dt;
        }



        //public DataSet SearchMoldDataByMoldCode(string prmMoldCode, string prmSection)
        //{
        //    string uid = "1";
        //    if (HttpContext.Current.Session["userInfo"] != null)
        //    {
        //        cms_app.Models.LoginModalData dm = (cms_app.Models.LoginModalData)HttpContext.Current.Session["userInfo"];
        //        uid = dm.userId;
        //    }

        //    string query = "<Data><Mold MoldName=\"" + prmMoldCode + "\" SearchBy=\"" + prmSection + "\" CreatedBy=\"" + uid + "\"></Mold></Data>";

        //    DataSet Ds = new DataSet();
        //    ExecuteOperation op = new ExecuteOperation();
        //    //Ds = op.SearchMoldDataByMoldCode(query, "[Admin].[SearchMoldDataByMoldCode]");
        //    Ds = op.SearchMoldDataByMoldCode(query, "[Admin].[Mold_SearchDataByMoldCode]");
        //    return Ds;
        //}

        public DataTable GetMoldList(string prmMoldType, Int32 prmActive, Int32 prmIsDelete)
        {
            DataTable Dt = new DataTable();
            ExecuteOperation op = new ExecuteOperation();
            Dt = op.GetMoldDataList(prmMoldType, prmActive, prmIsDelete);
            return Dt;
        }

        public DataTable GetMoldListForOrder(string prmProcName)
        {
            DataTable Dt = new DataTable();
            ExecuteOperation op = new ExecuteOperation();
            Dt = op.GetMoldDataListForOrder(prmProcName);
            return Dt;
        }
    }
}