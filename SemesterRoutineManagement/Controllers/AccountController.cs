using SemesterRoutineManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SemesterRoutineManagement.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        // GET: Account
        SemesterRoutineMSDBEntities db = new SemesterRoutineMSDBEntities();
        public ActionResult Login()
        {
            if (User.Identity.Name=="")
                return View();

            var objUser = db.Users.Where(x => x.UserName == User.Identity.Name).FirstOrDefault();
            if(objUser==null)
            {
                return View();

            }
            else
            {

                if (objUser.Role == "SuperAdmin")
                {
                    return RedirectToAction("Index", "Course");

                }
                
                else 
                {
                    return RedirectToAction("Index", "Course");

                }
            }

        }

        [HttpPost]
        public ActionResult Login(AccountModel model)
        {
            try
            {
                var user = db.Users.FirstOrDefault(x => x.UserName == model.UserName && x.Status == true);

                if (user != null && user.Password == model.Password)
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, false);

                    if (user.Role == "SuperAdmin" || user.Role == "Teacher" || user.Role == "Student")
                    {
                        return RedirectToAction("Index", "Course");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password.");
                }

                return View(model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while processing your request.");
                return View(model);
            }

        }
        public ActionResult Logout()
        {
            var objUser = db.Users.Where(x => x.UserName == User.Identity.Name).FirstOrDefault();
            if (objUser!=null)
            {
                FormsAuthentication.SignOut();
            }
            
            return RedirectToAction("Login");
        }
        public ActionResult Logouts()
        {
            FormsAuthentication.SignOut();
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            return View();
        }
    }
}