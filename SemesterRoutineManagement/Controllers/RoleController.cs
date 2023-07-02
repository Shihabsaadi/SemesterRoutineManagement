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
    public class RoleController : Controller
    {
        SemesterRoutineMSDBEntities db = new SemesterRoutineMSDBEntities();
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetRoleList()
        {
            List<Role> role = db.Roles.ToList();
            List<RoleModel> vm = role.Select(x => new RoleModel
            {
                Name = x.Name,
                Id = x.Id,
                Status = x.Status
            }).ToList();
            return Json(vm,JsonRequestBehavior.AllowGet);
        }
        public JsonResult SaveRole(RoleModel model)
        {
            var Message = "Action Failed";
            bool Success=false;
            try
            {
                if (model.Id > 0)
                {
                    Role updateDB = db.Roles.Find(model.Id);
                    updateDB.Name = model.Name;
                    updateDB.Status = model.Status;
                    db.Entry(updateDB).State = EntityState.Modified;
                    db.SaveChanges();
                    Message = model.Name + " Updated Successfully";
                }
                else
                {
                    Role roleDB = new Role();
                    roleDB.Name = model.Name;
                    roleDB.Status = true;
                    db.Roles.Add(roleDB);
                    db.SaveChanges();
                    Message = model.Name + " Added Successfully";
                }
                Success = true;
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                Success = false;
            }
            
            return Json(new{Message = Message, Success = Success }, JsonRequestBehavior.AllowGet);
        }
    }
}