using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentAttandanceLibrary.Models
{
    public partial class StudentGroup
    {
        public int StudentGroupId { get; set; }
        public string StudentId { get; set; } = null!;
        public int GroupId { get; set; }

        public virtual Group Group { get; set; } = null!;
        public virtual Student Student { get; set; } = null!;
    }
}
