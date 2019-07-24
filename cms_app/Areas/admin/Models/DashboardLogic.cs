using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace cms_app.Areas.admin.Models
{
    public class DashboardLogic
    {
        public DataTable GetDashboardReport()
        {
            DataTable Dt = new DataTable();
            ExecuteOperation op = new ExecuteOperation();
            Dt = op.GetDashboardReport();
            return Dt;
        }
    }
}