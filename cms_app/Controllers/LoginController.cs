using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;

namespace cms_app.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }


        /// <summary>
        /// Upload selected Excel files.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CheckLogin(FormCollection form)
        {
            try
            {
                var userId = form["txtUserId"].Trim();
                var userPass = form["txtPassword"].Trim();
                TempData["ErrorMessage"] = null;
                DataTable dtLogin = new DataTable();
                Models.LoginModal objDataManagement = new Models.LoginModal();

                //dtLogin = objDataManagement.LoginCredential(userId, Models.LoginModal.GenerateMD5(userPass));
                dtLogin = objDataManagement.LoginCredential(userId, userPass);
                if (dtLogin != null && dtLogin.Rows.Count > 0)
                {
                    var loginStatus = dtLogin.Rows[0]["LoginStatus"].ToString();
                    if (loginStatus == "A")
                    {
                        Models.LoginModalData d = new Models.LoginModalData();
                        d.userCode = dtLogin.Rows[0]["UserCode"].ToString();
                        d.userId = dtLogin.Rows[0]["UserId"].ToString();
                        d.userRole = dtLogin.Rows[0]["UserRole"].ToString();
                        d.CompanyCode = dtLogin.Rows[0]["CompanyCode"].ToString();
                        Session["userInfo"] = d;

                        //Session["userId"] = dtLogin.Rows[0]["UserId"];
                        //Session["userCode"] = dtLogin.Rows[0]["UserCode"];
                        //Session["userRole"] = dtLogin.Rows[0]["UserRole"];
                        return Redirect("~/admin/dashboard");
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "This user is no available more.";
                        return RedirectToAction("Index", "Login");
                    }
                }
                else
                {
                    TempData["ErrorMessage"] = "Login id or password is incorrect";
                    return RedirectToAction("Index", "Login");
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "There are some technical error Please contact administration.";
                return RedirectToAction("Index", "Login");
            }
        }
    }
}