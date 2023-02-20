using System.ComponentModel.DataAnnotations;

namespace StudentAttandanceLibrary.ModelValidation
{
    public class StudentList
    {
        [Required]
        public string FullName { get; set; }

        [Required]
        public string DOB { get; set; }

        [Required]
        public string Gender { get; set; }

        public string Address { get; set; }
    }
}
