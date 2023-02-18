using System;
using System.Collections.Generic;

namespace StudentAttandanceLibrary.Models
{
    public partial class Group
    {
        public Group()
        {
            Sessions = new HashSet<Session>();
            Students = new HashSet<Student>();
        }

        public int GroupId { get; set; }
        public string GroupName { get; set; } = null!;
        public string? TeacherId { get; set; }
        public int CourseId { get; set; }
        public int TermId { get; set; }

        public virtual Course Course { get; set; } = null!;
        public virtual Teacher? Teacher { get; set; }
        public virtual Term Term { get; set; } = null!;
        public virtual ICollection<Session> Sessions { get; set; }

        public virtual ICollection<Student> Students { get; set; }
    }
}
