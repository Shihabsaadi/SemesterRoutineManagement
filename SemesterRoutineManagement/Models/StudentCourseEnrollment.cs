//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SemesterRoutineManagement.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class StudentCourseEnrollment
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public Nullable<bool> Status { get; set; }
        public int SessionId { get; set; }
    
        public virtual Course Course { get; set; }
        public virtual User User { get; set; }
        public virtual Session Session { get; set; }
    }
}
