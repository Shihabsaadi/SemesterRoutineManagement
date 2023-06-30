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
                    return RedirectToAction("Index", "Role");

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
                bool isValid = db.Users.Any(x => x.UserName == model.UserName && x.Password == model.Password && x.Status == true);
                if (isValid)
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, false);
                    var user=db.Users.Where(x => x.UserName == model.UserName).Select(x => new {Role=x.Role}).FirstOrDefault();
                    if (user.Role == "SuperAdmin")
                    {
                        return RedirectToAction("Index", "Role");

                    }
                    else if (user.Role == "Teacher")
                    {
                        return RedirectToAction("Index", "Course");

                    }
                    else if (user.Role == "Student")
                    {
                        return RedirectToAction("Index", "Course");

                    }
                }
                ModelState.AddModelError("", "Invalid");
                return View();
            }
            catch (Exception ex)
            {

                throw;
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