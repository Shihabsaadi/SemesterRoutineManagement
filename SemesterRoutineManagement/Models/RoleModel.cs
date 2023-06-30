using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SemesterRoutineManagement.Models
{
    public class RoleModel
    {
        public int Id { get; set; }
        public int User_Id { get; set; }
        public string Name { get; set; }
        public Array RoleId { get; set; }
        public int Role_Id { get; set; }
        public bool Status { get; set; }

    }
}