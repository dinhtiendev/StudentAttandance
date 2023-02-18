using System;
using System.Collections.Generic;

namespace StudentAttandanceLibrary.Models
{
    public partial class Course
    {
        public Course()
        {
            Groups = new HashSet<Group>();
        }

        public int CourseId { get; set; }
        public string CourseCode { get; set; } = null!;
        public string CourseName { get; set; } = null!;

        public virtual ICollection<Group> Groups { get; set; }
    }
}
