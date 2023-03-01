using StudentAttandanceLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentAttandanceLibrary.ModelDtos
{
    public class AttandanceDto
    {
        public int AttendanceId { get; set; }
        public string StudentId { get; set; } = null!;
        public bool Present { get; set; }
        public string? Description { get; set; }
        public int SessionId { get; set; }
        public string GroupName { get; set; }
        public TimeSlot TimeSlot { get; set; }
        public int RoomId { get; set; }
        public string TeacherName { get; set; } = null!;
        public DateTime Date { get; set; }
        public int Index { get; set; }
        public bool? Attanded { get; set; }
    }
}
