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
    public class purchaseController : baseClass
    {
        // GET: user/purchase
        public ActionResult createorder()
        {
            return View();
        }

        // GET: user/purchase
        public ActionResult createinvoice()
        {
            return View();
        }

        [SessionExpire]
        public ActionResult orderlist()
        {
            return View();
        }

        [SessionExpire]
        public ActionResult invoicelist()
        {
            return View();
        }
    }
}