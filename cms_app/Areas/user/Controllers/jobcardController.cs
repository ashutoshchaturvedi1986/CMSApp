using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using cms_app.Areas.admin.Models;
using cms_app.Areas.user.Models;
using System.Data;
using Newtonsoft.Json;
using cms_app.Models.Common;

namespace cms_app.Areas.user.Controllers
{
    public class jobcardController : baseClass
    {
        string result = string.Empty;

        // GET: user/jobcard
        [SessionExpire]
        public ActionResult Index(string prmId)
        {
            if (!String.IsNullOrEmpty(prmId))
            {
                int id = Int32.TryParse(prmId, out id) ? id : 0;
                DataSet ds = new MasterDataLogic().GetDataWithCompanyForDropdown(id, "SAMPLECREATION");
                if (ds != null && ds.Tables.Count > 0)
                {
                    ViewData["dsData"] = ds;
                }
            }
            return View();
        }

        [SessionExpire]
        public ActionResult List()
        {
            return View();
        }
    }
}