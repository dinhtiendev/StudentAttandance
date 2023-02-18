using System;
using System.Collections.Generic;

namespace StudentAttandanceLibrary.Models
{
    public partial class Teacher
    {
        public Teacher()
        {
            Groups = new HashSet<Group>();
            Sessions = new HashSet<Session>();
        }

        public string TeacherId { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string? Image { get; set; }
        public DateTime Dob { get; set; }
        public bool Gender { get; set; }

        public virtual Account TeacherNavigation { get; set; } = null!;
        public virtual ICollection<Group> Groups { get; set; }
        public virtual ICollection<Session> Sessions { get; set; }
    }
}
