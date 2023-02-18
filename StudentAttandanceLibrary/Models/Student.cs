using System;
using System.Collections.Generic;

namespace StudentAttandanceLibrary.Models
{
    public partial class Student
    {
        public Student()
        {
            Attendances = new HashSet<Attendance>();
            Groups = new HashSet<Group>();
        }

        public string StudentId { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string? Image { get; set; }
        public DateTime Dob { get; set; }
        public bool Gender { get; set; }
        public string? Address { get; set; }

        public virtual Account StudentNavigation { get; set; } = null!;
        public virtual ICollection<Attendance> Attendances { get; set; }

        public virtual ICollection<Group> Groups { get; set; }
    }
}
