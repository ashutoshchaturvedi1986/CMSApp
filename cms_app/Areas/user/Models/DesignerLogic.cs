using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace cms_app.Areas.user.Models
{
    public class DesignerLogic
    {
        public DataTable GetPendingSampleList(string prmProcName)
        {
            DataTable Dt = new DataTable();
            ExecuteOperation op = new ExecuteOperation();
            Dt = op.GetPendingSampleList(prmProcName);
            return Dt;
        }

        public DataTable SaveSampleSubmition(String prmMoldId, String prmLastSubmitionDate, String prmAssignDate, String prmRemark,String prmIsApprove, out string result)
        {
            DataTable Dt = new DataTable();
            ExecuteOperation op = new ExecuteOperation();
            Dt = op.SaveSampleSubmition(prmMoldId, prmLastSubmitionDate, prmAssignDate, prmRemark, prmIsApprove, out result);
            return Dt;
        }


        public DataSet SaveCourierSubmition(String prmMoldId, String prmCourierId, String prmTransporterId, String prmCourierNo, String prmSamplePhoto,
             String prmRemark, String prmStatus, String prmAssignRemarks, String prmAssignDate, String prmDesignerId, String prmAction, out string result)
        {
            DataSet Ds = new DataSet();
            ExecuteOperation op = new ExecuteOperation();
            Ds = op.SaveCourierSubmition(prmMoldId, prmCourierId, prmTransporterId, prmCourierNo, prmSamplePhoto,prmRemark, prmStatus,
                prmAssignRemarks, prmAssignDate, prmDesignerId, prmAction, out result);
            return Ds;
        }
    }
}