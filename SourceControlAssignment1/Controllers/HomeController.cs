using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace SourceControlAssignment1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpPost]
        public ActionResult StudentDetails(Models.StudentModel sm)
        {
            if (string.IsNullOrEmpty(sm.Name))
            {
                ModelState.AddModelError("Name", "Name Required");
            }
            if (sm.Age == 0 || sm.Age > 120)
            {
                ModelState.AddModelError("Age", "Please Enter Valid Age between 1-120");
            }
            if (!string.IsNullOrEmpty(sm.Email))
            {
                string emailRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                                         @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                                            @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
                Regex re = new Regex(emailRegex);
                if (!re.IsMatch(sm.Email))
                {
                    ModelState.AddModelError("Email", "Email is not valid");
                }
            }
            else
            {
                ModelState.AddModelError("Email", "Email is required");
            }
            if (!string.IsNullOrEmpty(sm.ConfirmEmail))
            {
                string emailRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                                         @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                                            @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
                Regex re = new Regex(emailRegex);
                if (!re.IsMatch(sm.ConfirmEmail))
                {
                    ModelState.AddModelError("ConfirmEmail", "Email is not valid");
                }
                
            }
            else
            {
                ModelState.AddModelError("ConfirmEmail", "Email is required");
            }

            if (ModelState.IsValid)
            {
                ViewBag.name = sm.Name;
                ViewBag.email = sm.Email;
                ViewBag.PhoneNumber = sm.PhoneNumber;
                ViewBag.age = sm.Age;
                return View("Index");
            }
            else
            {
                ViewBag.name = "No Data";
                ViewBag.email = "No Data";
                ViewBag.PhoneNumber = "No Data";
                ViewBag.age = "No Data";
                return View("Index");
            }
        }
    }
}