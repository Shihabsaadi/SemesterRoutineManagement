using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SemesterRoutineManagement.Models
{
    public class TimeSpanModel
    {
        public int Id { get; set; }
        public string StartTimeFormated { get; set; }
        public string EndTimeFormated { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int Sort { get; set; }
        public Nullable<bool> Status { get; set; }
    }
}