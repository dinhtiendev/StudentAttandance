using System;
using System.Collections.Generic;

namespace StudentAttandanceLibrary.Models
{
    public partial class Session
    {
        public Session()
        {
            Attendances = new HashSet<Attendance>();
        }

        public int SessionId { get; set; }
        public int GroupId { get; set; }
        public int TimeSlotId { get; set; }
        public int RoomId { get; set; }
        public string TeacherId { get; set; } = null!;
        public DateTime Date { get; set; }
        public int Index { get; set; }
        public bool? Attanded { get; set; }

        public virtual Group Group { get; set; } = null!;
        public virtual Room Room { get; set; } = null!;
        public virtual Teacher Teacher { get; set; } = null!;
        public virtual TimeSlot TimeSlot { get; set; } = null!;
        public virtual ICollection<Attendance> Attendances { get; set; }
    }
}
