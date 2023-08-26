using SemesterRoutineManagement.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SemesterRoutineManagement.Controllers
{
    

    public class CourseController : Controller
    {
        // GET: Course
        public ActionResult Index()
        {
            return View();
        }
        SemesterRoutineMSDBEntities db = new SemesterRoutineMSDBEntities();

        public JsonResult GetCourseList()
        {
            List<Course> courses = db.Courses.ToList();
            var obj = db.Users.Where(x => x.UserName == User.Identity.Name).FirstOrDefault();
            List<CourseModel> vm=new List<CourseModel>();
            if (obj.Role == "SuperAdmin")
            {
               vm = courses.Select(x => new CourseModel
                {
                    Name = x.Name,
                    Id = x.Id,
                    Status = x.Status,
                    Code = x.Code,
                    CreatedAt = x.CreatedAt,
                    CreatedBy = x.CreatedBy
                }).ToList();
            }
           else if(obj.Role == "Teacher")
            {
                List<int> ids = db.TeacherAppointments.Where(x => x.TeacherId == obj.Id).Select(x => x.CourseId).ToList();
                vm = courses.Where(x=> ids.Contains(x.Id)).Select(x => new CourseModel
                {
                    Name = x.Name,
                    Id = x.Id,
                    Status = x.Status,
                    Code = x.Code,
                    CreatedAt = x.CreatedAt,
                    CreatedBy = x.CreatedBy
                }).ToList();
            }
            else if (obj.Role == "Student")
            {
                List<int> ids = db.StudentCourseEnrollments.Where(x => x.StudentId == obj.Id).Select(x => x.CourseId).ToList();
                vm = courses.Where(x => ids.Contains(x.Id)).Select(x => new CourseModel
                {
                    Name = x.Name,
                    Id = x.Id,
                    Status = x.Status,
                    Code = x.Code,
                    CreatedAt = x.CreatedAt,
                    CreatedBy = x.CreatedBy
                }).ToList();
            }
            return Json(vm, JsonRequestBehavior.AllowGet);
        }
        [Authorize(Roles = "SuperAdmin")]
        public JsonResult SaveCourse(CourseModel model)
        {
            var Message = "Action Failed";
            bool Success = false;
            var obj = db.Users.Where(x => x.UserName == User.Identity.Name).FirstOrDefault();
            try
            {
                if (model.Id > 0)
                {
                    Course updateDB = db.Courses.Find(model.Id);
                    updateDB.Name = model.Name;
                    updateDB.Code = model.Code;
                    updateDB.ModifedBy = obj.Id;
                    updateDB.ModifiedAt = DateTime.UtcNow;
                    updateDB.Status = model.Status;
                    db.Entry(updateDB).State = EntityState.Modified;
                    db.SaveChanges();
                    Message = model.Name + " Updated Successfully";
                }
                else
                {
                    //int getLastCourse = db.Courses.Count() > 0 ? db.Courses.OrderByDescending(x => x.Id).FirstOrDefault().Id + 1 : 1;
                    Course course = new Course();
                    course.Name = model.Name;
                    course.Code = model.Code;
                    course.Status = true;
                    course.CreatedBy = obj.Id;
                    course.CreatedAt = DateTime.UtcNow;
                    db.Courses.Add(course);
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
            
            return Json(new { Message = Message,Success=Success }, JsonRequestBehavior.AllowGet);
        }
    }
}