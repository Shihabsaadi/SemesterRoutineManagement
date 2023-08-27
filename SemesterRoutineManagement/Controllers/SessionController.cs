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
                Date = x.Date,
                Name = x.Name,
                Semester = (Semester)x.Semester,
                Id = x.Id,
                Status = x.Status
            }).ToList();
            return Json(vm, JsonRequestBehavior.AllowGet);
        }
        [AllowAnonymous]
        public JsonResult GetAvailableSessionList()
        {
            List<int> sessionIds = db.Routines.GroupBy(x => x.SessionId).Select(x=>(x.Key)).ToList();
            List<Session> session = db.Sessions.Where(x=> !sessionIds.Contains(x.Id)).ToList();
            List<SessionModel> vm = session.Select(x => new SessionModel
            {
                Date = x.Date,
                Name = x.Name,
                Semester = (Semester)x.Semester,
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
                var getYearName = model.Date.ToString("yy") + ((int)model.Semester + 1).ToString();
                if (db.Sessions.Any(x => x.Name.Contains(getYearName)))
                {
                    return Json(new { Message = "Already Exists!!!", Success = Success }, JsonRequestBehavior.AllowGet);
                }
                if (model.Id > 0)
                {
                    Session updateDB = db.Sessions.Find(model.Id);
                    updateDB.Name = getYearName;
                    updateDB.Status = model.Status;
                    updateDB.Semester = (int)model.Semester;
                    updateDB.Date = model.Date;
                    db.Entry(updateDB).State = EntityState.Modified;
                    db.SaveChanges();
                    Message = model.Name + " Updated Successfully";
                }
                else
                {
                    Session sessionDB = new Session();
                    sessionDB.Name = getYearName;
                    sessionDB.Status = true;
                    sessionDB.Semester = (int)model.Semester;
                    sessionDB.Date = model.Date;
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
