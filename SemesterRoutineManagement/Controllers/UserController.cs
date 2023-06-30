using SemesterRoutineManagement.Helper;
using SemesterRoutineManagement.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SemesterRoutineManagement.Controllers
{
    [Authorize(Roles = "SuperAdmin")]

    public class UserController : Controller
    {
        SemesterRoutineMSDBEntities db = new SemesterRoutineMSDBEntities();

        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetUserList()
        {
            List<User> user = db.Users.ToList();
            var obj = db.Users.Where(x => x.UserName == User.Identity.Name).FirstOrDefault();
            List<UserModel> vm = user.Select(x => new UserModel
            {
                Name = x.Name,
                Id = x.Id,
                Status = x.Status,
                Email= x.Email,
                phone= x.Phone,
                Role= x.Role,
                OwnAccount=x.Email==obj.Email?true:false,
            }).ToList();
            return Json(vm, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SaveUser(UserModel model)
        {
            var Message = "Action Failed";
            var obj = db.Users.Where(x => x.UserName == User.Identity.Name).FirstOrDefault();

            if (model.Id > 0)
            {
                User updateDB = db.Users.Find(model.Id);
                updateDB.Name = model.Name;
                updateDB.Status = model.Status;
                updateDB.Email = model.Email;
                updateDB.Role = model.Role;
                updateDB.Status = model.Status;
                updateDB.Phone = model.phone;
                db.Entry(updateDB).State = EntityState.Modified;
                db.SaveChanges();
                Message = model.Name + " Updated Successfully";
            }
            else
            {
                if (db.Users.Any(x=>x.Email==model.Email))
                    return Json(new { Message = "Already has a user with this mail" }, JsonRequestBehavior.AllowGet);
                User user = new User();
                user.Name = model.Name;
                user.Status = true;
                user.Email = model.Email;
                user.UserName=model.UserName;
                user.Password = Guid.NewGuid().ToString("N").Substring(0, 8);
                user.Role = model.Role;
                db.Users.Add(user);
                db.SaveChanges();
                Message = model.Name + " Added Successfully";
                try
                {
                    EmailSender.SendEmail(model.Email, "User has been created for Semesete Management", " Your User for Semster Routine Management has been created. The UserName: " + user.UserName + " & Password: " + user.Password + " ,your user has created as " + user.Role);
                }
                catch (Exception)
                {

                }
            }
            return Json(new { Message = Message }, JsonRequestBehavior.AllowGet);
        }
    
    }
}