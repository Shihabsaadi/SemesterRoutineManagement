using SemesterRoutineManagement.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace SemesterRoutineManagement.Controllers
{
    [Authorize(Roles = "SuperAdmin")]

    public class TimeSpanController : Controller
    {
        // GET: TimeSpan
        SemesterRoutineMSDBEntities db = new SemesterRoutineMSDBEntities();
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetTimeSpanList()
        {
            List<Models.TimeSpan> timeSpan = db.TimeSpans.ToList();
            List<TimeSpanModel> vm = timeSpan.Select(x => new TimeSpanModel
            {
                Id = x.Id,
                StartTimeFormated=x.startTime,
                EndTimeFormated=x.endTime,
                Sort=x.sort,
                Status = x.Status
            }).ToList();
            return Json(vm, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SaveTimeSpan(TimeSpanModel model)
        {
            var Message = "Action Failed";
            if (model.Id > 0)
            {
                Models.TimeSpan updateDB = db.TimeSpans.Find(model.Id);
                updateDB.startTime = model.StartTime.ToString("hh:mm tt");
                updateDB.endTime = model.EndTime.ToString("hh:mm tt");
                updateDB.sort = model.Sort;
                updateDB.Status = model.Status;
                db.Entry(updateDB).State = EntityState.Modified;
                db.SaveChanges();
                Message ="Updated Successfully";
            }
            else
            {
                Models.TimeSpan timeSpanDB = new Models.TimeSpan();
                timeSpanDB.startTime = model.StartTime.ToString("hh:mm tt");
                timeSpanDB.endTime = model.EndTime.ToString("hh:mm tt");
                timeSpanDB.sort = model.Sort;
                timeSpanDB.Status = true;
                db.TimeSpans.Add(timeSpanDB);
                db.SaveChanges();
                Message = " Added Successfully";
            }
            return Json(new { Message = Message }, JsonRequestBehavior.AllowGet);
        } 
        public JsonResult DeleteTimeSpan(TimeSpanModel model)
        {
            Models.TimeSpan deleteItem = db.TimeSpans.Find(model.Id);
            if (deleteItem == null)
                return Json(new { Message = "Not Found" }, JsonRequestBehavior.AllowGet);
            db.TimeSpans.Remove(deleteItem);
            db.SaveChanges();
            return Json(new { Message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);
        }
    }
}