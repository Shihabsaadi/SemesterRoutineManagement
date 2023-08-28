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
            List<int> studentIds=db.StudentCourseEnrollments.Where(s=>s.SessionId== model.SessionId && s.Term==model.Term).Select(x => x.StudentId).ToList();
            List<User> user = db.Users.Where(x => x.Role == "Student" && x.Status == true && !studentIds.Contains(x.Id)).ToList();
            List<UserModel> vm = user.Select(x => new UserModel
            {
                Name = x.Name + " - " + x.Email + " - "+ x.Phone,
                Id = x.Id,
                Selected=false
            }).ToList();
            return Json(vm, JsonRequestBehavior.AllowGet);
        }

        //public JsonResult GetTermList()
        //{
        //    List<Term> terms =new  List<Term>();
        //    List<CourseModel> vm = courses.Where(x => x.Status == true).Select(x => new CourseModel
        //    {
        //        Name = x.Name,
        //        Id = x.Id,
        //    }).ToList();
        //    return Json(vm, JsonRequestBehavior.AllowGet);
        //}
        public JsonResult GetStudentCourseEnrollmentList()
        {
            List<StudentCourseEnrollment> studentCourseEnrollments = db.StudentCourseEnrollments.ToList();
            List<StudentCourseEnrollmentModel> vm = studentCourseEnrollments.Where(x => x.Status == true).Select(x => new StudentCourseEnrollmentModel
            {
                Id = x.Id,
                StudentName = x.User.Name,
                StudentEmail = x.User.Email,
                StudentPhone = x.User.Phone,
                Term = x.Term,
                TermName = ((Term)x.Term).ToString(),
                Session=x.Session.Name
            }).ToList();
            return Json(vm, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Save(StudentCourseEnrollmentModel model)
        {
            var Message = "Action Failed";
            bool Success = false;
            try
            {
                var obj = db.Users.Where(x => x.UserName == User.Identity.Name).FirstOrDefault();
                StudentCourseEnrollment studentCourseEnrollment = new StudentCourseEnrollment();
                foreach (var studentId in model.StudentIds)
                {
                    studentCourseEnrollment.StudentId = studentId;
                    studentCourseEnrollment.Term = model.Term;
                    studentCourseEnrollment.SessionId = model.SessionId;
                    studentCourseEnrollment.Status = true;
                    db.StudentCourseEnrollments.Add(studentCourseEnrollment);
                    db.SaveChanges();
                }
                Message = " Added Successfully";
                Success = true;
            }
            catch (Exception ex)
            {

                Message =ex.Message;
                Success = false;
            }
            
            return Json(new { Message = Message,Success=Success }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteStudentCourseEnrollment(StudentCourseEnrollmentModel model)
        {
            var Message = "Action Failed";
            bool Success = false;
            try
            {
                StudentCourseEnrollment deleteItem = db.StudentCourseEnrollments.Find(model.Id);
                if (deleteItem == null)
                    return Json(new { Message = "Not Found" }, JsonRequestBehavior.AllowGet);
                db.StudentCourseEnrollments.Remove(deleteItem);
                db.SaveChanges();
                Message = "Deleted Successfully";
                Success = true;
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                Success = false;
            }
           
            return Json(new { Message = Message, Success=Success}, JsonRequestBehavior.AllowGet);
        }
    }
}