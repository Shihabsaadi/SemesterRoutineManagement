using SemesterRoutineManagement.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SemesterRoutineManagement.Controllers
{
    [Authorize(Roles = "SuperAdmin")]

    public class SessionController : Controller
    {
        // GET: Session
        SemesterRoutineMSDBEntities db = new SemesterRoutineMSDBEntities();
        public ActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        public JsonResult GetSessionList()
        {
            List<Session> session = db.Sessions.ToList();
            List<SessionModel> vm = session.Select(x => new SessionModel
            {
                Name = x.Name,
                Id = x.Id,
                Status = x.Status
            }).ToList();
            return Json(vm, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SaveSession(SessionModel model)
        {
            var Message = "Action Failed";
            bool Success = false;
            try
            {
                if (model.Id > 0)
                {
                    Session updateDB = db.Sessions.Find(model.Id);
                    updateDB.Name = model.Name;
                    updateDB.Status = model.Status;
                    db.Entry(updateDB).State = EntityState.Modified;
                    db.SaveChanges();
                    Message = model.Name + " Updated Successfully";
                }
                else
                {
                    Session sessionDB = new Session();
                    sessionDB.Name = model.Name;
                    sessionDB.Status = true;
                    db.Sessions.Add(sessionDB);
                    db.SaveChanges();
                    Message = model.Name + " Added Successfully";
                }
                Success = true;
            }
            catch (Exception ex)
            {
                Message = ex.Message    ;
                Success = false;
            }
            
            return Json(new { Message = Message,Success=Success }, JsonRequestBehavior.AllowGet);
        }
    }
}
