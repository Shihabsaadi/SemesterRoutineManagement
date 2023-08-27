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
                    TermName = ((Term)x.Term).ToString(),
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
                Routines = db.Routines.Where(x => x.TeacherId == obj.Id).ToList();
                vm = Routines.Where(x => x.SessionId == model.SessionId).Select(x => new RoutineModel
                {
                    Id = x.Id,
                    SessionName = x.Session.Name,
                    TermName = ((Term)x.Term).ToString(),
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
                    TermName = ((Term)x.Term).ToString(),
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
        public JsonResult Save(RoutineModel model)
        {
            var Message = "Action Failed";
            bool Success = false;
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
                    RoutineDB.Term = item.Term;

                    db.Routines.Add(RoutineDB);
                    db.SaveChanges();
                }
                Message = " Added Successfully";
                Success = true;
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                Success = false;
            }


            return Json(new { Message = Message, Success = Success }, JsonRequestBehavior.AllowGet);
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
        public JsonResult GenerateRoutine(int sessionOf)
        {
            var courses = new List<Course>();
            courses = sessionOf == 1 ? db.Courses.Where(x => x.Status == true && (x.Term == ((int)Term.FirstYearFirstSemester)
           || x.Term == ((int)Term.SecondYearFirstSemester) || x.Term == ((int)Term.ThirdYearFirstSemester) || x.Term == ((int)Term.FourthYearFirstSemester)
           )).OrderBy(x => x.Term).ToList() : db.Courses.Where(x => x.Status == true && (x.Term == ((int)Term.FirstYearSecondSemester)
           || x.Term == ((int)Term.SecondYearSecondSemester) || x.Term == ((int)Term.ThirdYearSecondSemester) || x.Term == ((int)Term.FourthYearSecondSemester)
           )).OrderBy(x => x.Term).ToList();            //courses=Shuffle(courses);
            var courseIds = courses.Select(x => x.Id).ToList();

            var weekDays = db.WeekDays.Where(x => x.Status == true).Select(x => new
            {
                Id = x.Id,
                Name = x.Name,
                Sort = x.Sort
            }).OrderBy(s => s.Sort).ToList();

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
                Teacher = x.User.Name,
                Course = x.Course.Name,
                CourseId = x.CourseId,
                Id = x.Id
            }).ToList();
            //List<RoutineModel> routineModels = new List<RoutineModel>();
            List<RoutineModel> generatedRoutine = new List<RoutineModel>();
            Random random = new Random();

            foreach (var weekDay in weekDays)
            {
                var availableTeachers = teacherCourses.ToList();
                var availableCourses = courses.ToList();
                var availableTimeSpans = timeSpans.ToList();
                var terms = sessionOf == 1 ? new List<int> { 0, 2, 4, 6 } : new List<int> { 1, 3, 5, 7 };
                try
                {
                    foreach (var timeSlot in timeSpans)
                    {
                        var availableRooms = rooms.ToList();

                        foreach (var term in terms)
                        {
                            var termCourses = courses.Where(course => course.Term == term).ToList();

                            if (termCourses.Count > 0 && availableRooms.Count > 0 && teacherCourses.Count > 0)
                            {
                                var teacherIndex = random.Next(availableTeachers.Count);
                                //var courseIndex = random.Next(availableCourses.Count);
                                var timeSpanIndex = random.Next(availableTimeSpans.Count);

                                var teacher = availableTeachers[teacherIndex];
                                //var course = availableCourses[courseIndex];
                                var _timeSpan = availableTimeSpans[timeSpanIndex];

                                var courseIndex = random.Next(termCourses.Count); // Start with the first course in the list
                                var roomIndex = random.Next(availableRooms.Count); // Start with the first room in the list
                                var room = availableRooms[roomIndex];

                                var roomConflict = false;
                                roomConflict = generatedRoutine.Any(x => x.StartTime == timeSlot.Start && x.RoomId == room.Id) ? true : false;
                                while (roomConflict)
                                {
                                    roomIndex = random.Next(availableRooms.Count); // Start with the first room in the list
                                    room = availableRooms[roomIndex];
                                    roomConflict = generatedRoutine.Any(x => x.StartTime == timeSlot.Start && x.RoomId == room.Id && x.DayId== weekDay.Id) ? true : false;
                                }
                                var course = termCourses[courseIndex];
                                var courseConflict = false;
                                courseConflict = generatedRoutine.Any(x => x.CourseId==course.Id) ? true : false;
                                var removedterm = course.Term;
                                var courseInvocked = false;
                                var maxClassInADay = false;

                                if (generatedRoutine.Where(x => x.Term == course.Term && x.DayId == weekDay.Id).Count() == 5)
                                {
                                    maxClassInADay = true;
                                }
                                var getTermCourse = availableCourses.Where(x => x.Term == removedterm).ToList();
                                if (getTermCourse.Count() != generatedRoutine.Where(x => x.Term == removedterm).Count())
                                {
                                    while (courseConflict)
                                    {
                                        courseIndex = random.Next(getTermCourse.Count);
                                        course = getTermCourse[courseIndex];
                                        courseConflict = generatedRoutine.Any(x => x.CourseId == course.Id) ? true : false;
                                    }
                                }
                                else
                                {
                                    courseInvocked = true;
                                }

                                if (!courseInvocked && !maxClassInADay)
                                {
                                    var teacherConflict = false;
                                    var availableTeachersInCourse = availableTeachers.Where(x => x.CourseId == course.Id).ToList();
                                    teacherIndex = random.Next(availableTeachersInCourse.Count); // Start with the first room in the list
                                    teacher = availableTeachersInCourse[teacherIndex];
                                    teacherConflict = generatedRoutine.Any(x => x.TeacherId == teacher.TeacherId && x.StartTime==timeSlot.Start && x.DayId == weekDay.Id) ? true : false;
                                    while (teacherConflict)
                                    {
                                        teacherIndex = random.Next(availableTeachersInCourse.Count); // Start with the first room in the list
                                        teacher = availableTeachersInCourse[teacherIndex];
                                        teacherConflict = generatedRoutine.Any(x => x.TeacherId == teacher.TeacherId && x.StartTime == timeSlot.Start && x.DayId == weekDay.Id) ? true : false;
                                    }
                                    generatedRoutine.Add(new RoutineModel
                                    {
                                        DayId = weekDay.Id,
                                        Day = weekDay.Name,
                                        Term = course.Term,
                                        StartTime = timeSlot.Start,
                                        EndTime = timeSlot.End,
                                        TeacherName = teacher.Teacher,
                                        CourseName = course.Name,
                                        CourseCode = course.Code,
                                        TermName=((Term)course.Term).ToString(),
                                        TimeSpanId = timeSlot.Id,
                                        TeacherId = teacher.TeacherId,
                                        CourseId = course.Id/* course.Id*/,
                                        RoomId = room.Id,
                                        RoomNo = room.No
                                    });
                                    // Update the indices to move to the next course and room
                                    courseIndex = (courseIndex + 1) % termCourses.Count;
                                    roomIndex = (roomIndex + 1) % availableRooms.Count;
                                    teacherIndex = (teacherIndex + 1) % teacherCourses.Count;
                                }
                                   
                          
                            }
                        }
                    }

                }
                catch (Exception ex)
                {

                }

            }
            return Json(generatedRoutine, JsonRequestBehavior.AllowGet);

        }
    }
}