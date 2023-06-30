using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SemesterRoutineManagement.Models
{
    public class TeacherAppointmentModel
    {
        public int Id { get; set; }
        public int TeacherId { get; set; }
        public String TeacherName { get; set; }
        public String TeacherEmail { get; set; }
        public String TeacherPhone { get; set; }
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public string CourseCode { get; set; }
        public bool? Status { get; set; }
    }
}