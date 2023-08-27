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

    public class WeekdayController : Controller
    {
        // GET: Room
        SemesterRoutineMSDBEntities db = new SemesterRoutineMSDBEntities();
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetWeekdayList()
        {
            List<WeekDay> weeks = db.WeekDays.ToList();
            List<WeekDayModel> vm = weeks.Select(x => new WeekDayModel
            {
                Id = x.Id,
                Name = x.Name,
                ShortName = x.ShortName,
                Sort = x.Sort,
                Status = x.Status
            }).ToList();
            return Json(vm, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SaveWeekDay(WeekDayModel model)
        {
           
            var Message = "Action Failed";
            bool Success = false;
            if(db.WeekDays.Any(x=>x.Id!=model.Id && (x.Name==model.Name || x.ShortName == model.ShortName || x.Sort ==model.Sort)))
                return Json(new { Message = "Already exists!!!" , Success = Success }, JsonRequestBehavior.AllowGet);
            try
            {
                if (model.Id > 0)
                {
                    WeekDay updateDB = db.WeekDays.Find(model.Id);
                    updateDB.Name = model.Name;
                    updateDB.ShortName = model.ShortName;
                    updateDB.Sort = model.Sort;
                    updateDB.Status = model.Status;
                    db.Entry(updateDB).State = EntityState.Modified;
                    db.SaveChanges();
                    Message = model.Name + " Updated Successfully";
                }
                else
                {
                    WeekDay WeekDayDB = new WeekDay();
                    WeekDayDB.Name = model.Name;
                    WeekDayDB.ShortName = model.ShortName;
                    WeekDayDB.Sort = model.Sort;
                    WeekDayDB.Status = true;
                    db.WeekDays.Add(WeekDayDB);
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
        
            return Json(new { Message = Message, Success = Success }, JsonRequestBehavior.AllowGet);
        }
    }
}