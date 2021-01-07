using MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using log4net;

namespace MVC.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        readonly ILog log = log4net.LogManager.GetLogger(typeof(AccountController));
        // GET: Account
        public ActionResult Login()
        {
            Session.Remove("UserID");
            return View();
        }

        [HttpPost]
        public ActionResult Login(Models.UserDetail model)
        {

            using (var context = new ProductmanagmentDBEntities())
            {
                bool isValid = context.UserDetails.Any(x => x.EmailID == model.EmailID && x.Password == model.Password);
                if (isValid)
                {
                    Session["UserID"] = model.UserID.ToString();
                    FormsAuthentication.SetAuthCookie(model.EmailID, true);
                    log.Info("User:" + model.EmailID + " Login at " + DateTime.Now.ToString());
                    return RedirectToAction("Welcome", "ProductDetailsMVC");
                }
                ModelState.AddModelError("", "Invalid Username and Password");
                return View();
            }

        }

        public ActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Signup(UserDetail model)
        {
            using (var context = new ProductmanagmentDBEntities())
            {
                context.UserDetails.Add(model);
                log.Info("User:" + model.EmailID + " Signin at " + DateTime.Now.ToString());
                context.SaveChanges();
            }
            return RedirectToAction("login");
        }
        public ActionResult Logout()
        {
            Session.Remove("UserID");
            FormsAuthentication.SignOut();
            log.Info("User Logout at " + DateTime.Now.ToString());
            return RedirectToAction("Login");
        }
    }
}

