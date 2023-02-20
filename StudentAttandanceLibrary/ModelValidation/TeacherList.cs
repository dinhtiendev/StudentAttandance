using System.ComponentModel.DataAnnotations;

namespace StudentAttandanceLibrary.ModelValidation
{
    public class TeacherList
    {
        [Required]
        public string FullName { get; set; }

        [Required]
        public string DOB { get; set; }

        [Required]
        public string Gender { get; set; }
    }
}
