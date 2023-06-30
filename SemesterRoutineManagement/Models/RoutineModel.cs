using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SemesterRoutineManagement.Models
{
    public class RoutineModel
    {
        public int Id { get; set; }
        public string CourseName { get; set; }
        public int CourseId { get; set; }
        public string TeacherName { get; set; }
        public int TeacherId { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public int TimeSpanId { get; set; }
        public string Day { get; set; }
        public int DayId { get; set; }
        public int SessionId { get; set; }
        public string SessionName { get; set; }
        public int RoomId { get; set; }
        public string RoomNo { get; set; }
        public List<RoutineModel> Routines { get; set; }
        public int Sort { get; set; }
    }
}