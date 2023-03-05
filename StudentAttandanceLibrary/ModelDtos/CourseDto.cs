using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentAttandanceLibrary.ModelDtos
{
    public class CourseDto
    {
        public int CourseId { get; set; }
        public string? CourseCode { get; set; }
        public string? CourseName { get; set; }
        public string? GroupName { get; set; }
        public DateTime StartDate { get; set; }
    }
}
