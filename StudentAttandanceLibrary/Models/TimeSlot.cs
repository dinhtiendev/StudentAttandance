using System;
using System.Collections.Generic;

namespace StudentAttandanceLibrary.Models
{
    public partial class TimeSlot
    {
        public TimeSlot()
        {
            Sessions = new HashSet<Session>();
        }

        public int TimeSlotId { get; set; }
        public string Description { get; set; } = null!;

        public virtual ICollection<Session> Sessions { get; set; }
    }
}
