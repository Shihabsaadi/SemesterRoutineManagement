using SemesterRoutineManagement.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SemesterRoutineManagement.Controllers
{
    public class RoutineController : Controller
    {
        SemesterRoutineMSDBEntities db = new SemesterRoutineMSDBEntities();

        // GET: Routine
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetRoutineList(RoutineModel model)
        {
            var obj = db.Users.Where(x => x.UserName == User.Identity.Name).FirstOrDefault();
            List<RoutineModel> vm = new List<RoutineModel>();
            List<Routine> Routines = db.Routines.ToList();
            if (obj.Role == "SuperAdmin")
            {
             vm = Routines.Where(x => x.SessionId == model.SessionId).Select(x => new RoutineModel
                {
                    Id = x.Id,
                    SessionName = x.Session.Name,
                    RoomNo = x.Room.No,
                    StartTime = x.TimeSpan.startTime,
                    EndTime = x.TimeSpan.endTime,
                    CourseName = x.Course.Name,
                    Day = x.WeekDay.Name,
                    TeacherName = x.User.Name,
                    Sort = x.WeekDay.Sort
                }).OrderBy(x => x.Sort).ToList();
            }
            else if (obj.Role == "Teacher")
            {
                Routines = db.Routines.Where(x=>x.TeacherId==obj.Id).ToList();
                vm = Routines.Where(x => x.SessionId == model.SessionId).Select(x => new RoutineModel
                {
                    Id = x.Id,
                    SessionName = x.Session.Name,
                    RoomNo = x.Room.No,
                    StartTime = x.TimeSpan.startTime,
                    EndTime = x.TimeSpan.endTime,
                    CourseName = x.Course.Name,
                    Day = x.WeekDay.Name,
                    TeacherName = x.User.Name,
                    Sort = x.WeekDay.Sort
                }).OrderBy(x => x.Sort).ToList();
            }
            else if (obj.Role == "Student")
            {
                List<int> ids = db.StudentCourseEnrollments.Where(x => x.StudentId == obj.Id).Select(x => x.CourseId).ToList();
                Routines = db.Routines.Where(x => ids.Contains(x.CourseId)).ToList();
                vm = Routines.Where(x => x.SessionId == model.SessionId).Select(x => new RoutineModel
                {
                    Id = x.Id,
                    SessionName = x.Session.Name,
                    RoomNo = x.Room.No,
                    StartTime = x.TimeSpan.startTime,
                    EndTime = x.TimeSpan.endTime,
                    CourseName = x.Course.Name,
                    Day = x.WeekDay.Name,
                    TeacherName = x.User.Name,
                    Sort = x.WeekDay.Sort
                }).OrderBy(x => x.Sort).ToList();
            }

            return Json(vm, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GenerateRoutine()
        {
            List<Course> courses = db.Courses.Where(x => x.Status == true && (x.Term == ((int)Term.FirstYearFirstSemester)
            || x.Term == ((int)Term.SecondYearFirstSemester) || x.Term == ((int)Term.ThirdYearFirstSemester) || x.Term == ((int)Term.FourthYearFirstSemester)
            )).ToList();            //courses=Shuffle(courses);
            var courseIds=courses.Select(x=>x.Id).ToList();

            var weekDays= db.WeekDays.Where(x=>x.Status==true).Select(x => new
            {
                Id=x.Id,
                Name=x.Name,
                Sort=x.Sort
            }).OrderBy(s=>s.Sort).ToList();

            var rooms = db.Rooms.Where(x => x.Status == true).Select(x => new
            {
                Id = x.Id,
                No = x.No
            }).ToList();

            var timeSpans = db.TimeSpans.Select(x => new
            {
                Id = x.Id,
                Start = x.startTime,
                End = x.endTime,
                Sort = x.sort
            }).OrderBy(s => s.Sort).ToList();

            var teacherCourses = db.TeacherAppointments.Where(x => courseIds.Contains(x.CourseId)).Select(x => new
            {
                TeacherId = x.TeacherId,
                Teacher=x.User.Name,
                Course=x.Course.Name,
                CourseId = x.CourseId,
                Id = x.Id
            }).ToList();
            //List<RoutineModel> routineModels = new List<RoutineModel>();
            List<RoutineModel> generatedRoutine = new List<RoutineModel>();
            Random random = new Random();

            foreach (var weekDay in weekDays)
            {
                foreach (var timeSpan in timeSpans)
                {
                    var availableTeachers = teacherCourses.ToList();
                    var availableCourses = courses.ToList();
                    var availableTimeSpans = timeSpans.ToList();

                    while (availableTeachers.Count > 0 && availableCourses.Count > 0 && availableTimeSpans.Count > 0)
                    {
                        var teacherIndex = random.Next(availableTeachers.Count);
                        //var courseIndex = random.Next(availableCourses.Count);
                        var timeSpanIndex = random.Next(availableTimeSpans.Count);

                        var teacher = availableTeachers[teacherIndex];
                        //var course = availableCourses[courseIndex];
                        var _timeSpan = availableTimeSpans[timeSpanIndex];

                        if (!generatedRoutine.Any(routine =>
                            routine.DayId == weekDay.Id &&
                            routine.TimeSpanId == _timeSpan.Id &&
                            routine.TeacherId == teacher.Id /*&&*/
                            /*routine.CourseId == course.Id*/))
                        {
                            var room = rooms[random.Next(rooms.Count)];
                            if(!generatedRoutine.Any(x=>x.DayId==weekDay.Id && x.CourseId== teacher.CourseId/* course.Id*/))
                            {
                                if (!generatedRoutine.Any(x => x.DayId == weekDay.Id && x.TimeSpanId == timeSpan.Id && x.RoomId == room.Id))
                                {
                                    if (!generatedRoutine.Any(x => x.DayId == weekDay.Id && x.TimeSpanId == timeSpan.Id && x.TeacherId == teacher.TeacherId))
                                    {
                                        if (generatedRoutine.Where(x => x.CourseId == teacher.CourseId /*course.Id*/).Count() < 1)
                                        {
                                            generatedRoutine.Add(new RoutineModel
                                            {
                                                DayId = weekDay.Id,
                                                Day = weekDay.Name,
                                                StartTime = timeSpan.Start,
                                                EndTime = timeSpan.End,
                                                TeacherName = teacher.Teacher,
                                                CourseName =teacher.Course /*course.Name*/,
                                                TimeSpanId = timeSpan.Id,
                                                TeacherId = teacher.TeacherId,
                                                CourseId =teacher.CourseId/* course.Id*/,
                                                RoomId = room.Id,
                                                RoomNo = room.No
                                            });
                                            
                                        }
                                    }
                                }
                            }
                            availableTeachers.RemoveAt(teacherIndex);
                            //availableCourses.RemoveAt(courseIndex);
                            availableTimeSpans.RemoveAt(timeSpanIndex);
                        }
                    }
                }
            }

            //foreach (var weekday in weekdays)
            //{
            //    foreach (var timeSpan in timeSpans)
            //    {
            //        foreach (var course in courses)
            //        {
            //          foreach (var room in rooms)
            //            {
            //                if (routineModels.Any(x => x.RoomId == room.Id && x.CourseId == course.Id && x.StartTime == timeSpan.Start && x.EndTime == timeSpan.End))
            //                {

            //                }
            //                else
            //                {

            //                }
            //            }
            //        }
            //    }
            //}

            return Json(generatedRoutine, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Save(RoutineModel model)
        {
            var Message = "Action Failed";
            bool Success=false;
            try
            {
                Routine RoutineDB = new Routine();
                foreach (var item in model.Routines)
                {
                    RoutineDB.SessionId = model.SessionId;
                    RoutineDB.CourseId = item.CourseId;
                    RoutineDB.TimeSpanId = item.TimeSpanId;
                    RoutineDB.WeekDayId = item.DayId;
                    RoutineDB.RoomId = item.RoomId;
                    RoutineDB.TeacherId = item.TeacherId;
                    db.Routines.Add(RoutineDB);
                    db.SaveChanges();
                }
                Message = " Added Successfully";
                Success = true;
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                Success=false;
            }
            

            return Json(new { Message = Message,Success=Success }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteRoutine(RoutineModel model)
        {
            Routine deleteItem = db.Routines.Find(model.Id);
            if (deleteItem == null)
                return Json(new { Message = "Not Found" }, JsonRequestBehavior.AllowGet);
            db.Routines.Remove(deleteItem);
            db.SaveChanges();
            return Json(new { Message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);
        }
        //static List<T> Shuffle<T>(List<T> list)
        //{
        //    Random random = new Random();
        //    return list.OrderBy(x => random.Next()).ToList();
        //}
    }
}