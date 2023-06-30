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
    public class TeacherAppointmentController : Controller
    {
        SemesterRoutineMSDBEntities db = new SemesterRoutineMSDBEntities();

        public ActionResult Index()
        {
            return View();
        }
        // GET: TeacherAppointment

        public JsonResult GetTeacherList()
        {
            List<User> user = db.Users.Where(x=>x.Role=="Teacher" && x.Status==true).ToList();
            List<UserModel> vm = user.Select(x => new UserModel
            {
                Name = x.Name,
                Id = x.Id,
            }).ToList();
            return Json(vm, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetCourseList()
        {
            List<Course> courses = db.Courses.ToList();
            List<CourseModel> vm = courses.Where(x=>x.Status==true).Select(x => new CourseModel
            {
                Name = x.Name,
                Id = x.Id,
            }).ToList();
            return Json(vm, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetTeacherAppointmentList()
        {
            List<TeacherAppointment> teacherAppointments = db.TeacherAppointments.ToList();
            List<TeacherAppointmentModel> vm = teacherAppointments.Select(x => new TeacherAppointmentModel
            {
                Id=x.Id,
                TeacherId=x.TeacherId,
                TeacherName = x.User.Name,
                TeacherEmail = x.User.Email,
                TeacherPhone = x.User.Phone,
                CourseId=x.CourseId,
                CourseName=x.Course.Name,
                CourseCode=x.Course.Code,
                Status=x.Status
            }).OrderByDescending(x=>x.Status).ToList();
            return Json(vm, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Save(TeacherAppointmentModel model)
        {
            var Message = "Action Failed";
            try
            {
                if (model.Id > 0)
                {
                    TeacherAppointment updateDB = db.TeacherAppointments.Find(model.Id);
                    updateDB.CourseId = model.CourseId;
                    updateDB.TeacherId = model.TeacherId;
                    updateDB.Status = model.Status;
                    db.Entry(updateDB).State = EntityState.Modified;
                    db.SaveChanges();
                    Message = " Updated Successfully";
                }
                else
                {
                    if(db.TeacherAppointments.Any(x=>x.TeacherId==model.TeacherId && x.CourseId==model.CourseId))
                        return Json(new { Message = "Already Assigned" }, JsonRequestBehavior.AllowGet);
                    TeacherAppointment teacherAppointment = new TeacherAppointment();
                    teacherAppointment.CourseId = model.CourseId;
                    teacherAppointment.TeacherId = model.TeacherId;
                    teacherAppointment.Status = true;
                    db.TeacherAppointments.Add(teacherAppointment);
                    db.SaveChanges();
                    Message = " Added Successfully";
                }
                return Json(new { Message = Message }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                Message =ex.Message;
                return Json(new { Message = Message }, JsonRequestBehavior.AllowGet);
            }
          
        }

    }
}