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

    public class RoomController : Controller
    {
        // GET: Room
        SemesterRoutineMSDBEntities db = new SemesterRoutineMSDBEntities();
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetRoomList()
        {
            List<Room> Room = db.Rooms.ToList();
            List<RoomModel> vm = Room.Select(x => new RoomModel
            {
                Id = x.Id,
                No = x.No,
                Status = x.Status
            }).ToList();
            return Json(vm, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SaveRoom(RoomModel model)
        {
            var Message = "Action Failed";
            if (model.Id > 0)
            {
                Room updateDB = db.Rooms.Find(model.Id);
                updateDB.No= model.No;
                updateDB.Status = model.Status;
                db.Entry(updateDB).State = EntityState.Modified;
                db.SaveChanges();
                Message = model.No + " Updated Successfully";
            }
            else
            {
                Room RoomDB = new Room();
                RoomDB.No = model.No;
                RoomDB.Status = true;
                db.Rooms.Add(RoomDB);
                db.SaveChanges();
                Message = model.No + " Added Successfully";
            }
            return Json(new { Message = Message }, JsonRequestBehavior.AllowGet);
        }
    }
}