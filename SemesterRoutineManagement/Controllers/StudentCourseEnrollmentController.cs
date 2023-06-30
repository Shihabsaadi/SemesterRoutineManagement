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

    public class StudentCourseEnrollmentController : Controller
    {
        // GET: StudentCourseEnrollment
        SemesterRoutineMSDBEntities db = new SemesterRoutineMSDBEntities();
        public ActionResult Index()
        {
            return View();
        }
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
        public JsonResult GetAvailableStudentList(StudentCourseEnrollmentModel model)
        {
            List<int> studentIds=db.StudentCourseEnrollments.Where(s=>s.SessionId== model.SessionId && s.CourseId==model.CourseId).Select(x => x.StudentId).ToList();
            List<User> user = db.Users.Where(x => x.Role == "Student" && x.Status == true && !studentIds.Contains(x.Id)).ToList();
            List<UserModel> vm = user.Select(x => new UserModel
            {
                Name = x.Name + " - " + x.Email + " - "+ x.Phone,
                Id = x.Id,
                Selected=false
            }).ToList();
            return Json(vm, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCourseList()
        {
            List<Course> courses = db.Courses.ToList();
            List<CourseModel> vm = courses.Where(x => x.Status == true).Select(x => new CourseModel
            {
                Name = x.Name,
                Id = x.Id,
            }).ToList();
            return Json(vm, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetStudentCourseEnrollmentList()
        {
            List<StudentCourseEnrollment> studentCourseEnrollments = db.StudentCourseEnrollments.ToList();
            List<StudentCourseEnrollmentModel> vm = studentCourseEnrollments.Where(x => x.Status == true).Select(x => new StudentCourseEnrollmentModel
            {
                Id = x.Id,
                StudentName = x.User.Name,
                StudentEmail = x.User.Email,
                StudentPhone = x.User.Phone,
                Course=x.Course.Name,
                Session=x.Session.Name
            }).ToList();
            return Json(vm, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Save(StudentCourseEnrollmentModel model)
        {
            var Message = "Action Failed";
            var obj = db.Users.Where(x => x.UserName == User.Identity.Name).FirstOrDefault();
            StudentCourseEnrollment studentCourseEnrollment = new StudentCourseEnrollment();
            foreach (var studentId in model.StudentIds)
            {
                studentCourseEnrollment.StudentId = studentId;
                studentCourseEnrollment.CourseId= model.CourseId;
                studentCourseEnrollment.SessionId= model.SessionId;
                studentCourseEnrollment.Status = true;
                db.StudentCourseEnrollments.Add(studentCourseEnrollment);
                db.SaveChanges();
            }
            Message =" Added Successfully";
            return Json(new { Message = Message }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteStudentCourseEnrollment(StudentCourseEnrollmentModel model)
        {
            StudentCourseEnrollment deleteItem = db.StudentCourseEnrollments.Find(model.Id);
            if (deleteItem == null)
                return Json(new { Message = "Not Found" }, JsonRequestBehavior.AllowGet);
            db.StudentCourseEnrollments.Remove(deleteItem);
            db.SaveChanges();
            return Json(new { Message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);
        }
    }
}