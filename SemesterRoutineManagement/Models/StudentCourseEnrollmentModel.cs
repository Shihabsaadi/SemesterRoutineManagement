using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SemesterRoutineManagement.Models
{
    public class StudentCourseEnrollmentModel
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public Nullable<bool> Status { get; set; }
        public int SessionId { get; set; }
        public List<int> StudentIds { get; set; }
        public string StudentName { get;set; }
        public string StudentPhone { get; set; }
        public string StudentEmail { get; set; }
        public string Course { get; set; }
        public string Session { get; set; }


    }
}