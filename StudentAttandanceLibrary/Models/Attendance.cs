using System;
using System.Collections.Generic;

namespace StudentAttandanceLibrary.Models
{
    public partial class Attendance
    {
        public int AttendanceId { get; set; }
        public int SessionId { get; set; }
        public string StudentId { get; set; } = null!;
        public bool Present { get; set; }
        public string? Description { get; set; }

        public virtual Session Session { get; set; } = null!;
        public virtual Student Student { get; set; } = null!;
    }
}
