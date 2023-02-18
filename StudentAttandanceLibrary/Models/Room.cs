using System;
using System.Collections.Generic;

namespace StudentAttandanceLibrary.Models
{
    public partial class Room
    {
        public Room()
        {
            Sessions = new HashSet<Session>();
        }

        public int RoomId { get; set; }
        public string RoomName { get; set; } = null!;

        public virtual ICollection<Session> Sessions { get; set; }
    }
}
