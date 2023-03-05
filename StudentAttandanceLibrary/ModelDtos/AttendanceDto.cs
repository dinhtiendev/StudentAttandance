using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentAttandanceLibrary.ModelDtos
{
    public class AttendanceDto
    {
        public int AttendanceId { get; set; }

        public int SessionId { get; set; }

        public bool Present { get; set; }

        public string? Description { get; set; }

        public DateTime Date { get; set; }
    }
}
