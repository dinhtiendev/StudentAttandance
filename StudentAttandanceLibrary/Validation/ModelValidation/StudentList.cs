using StudentAttandanceLibrary.Validation.CustomValidation;
using System.ComponentModel.DataAnnotations;

namespace StudentAttandanceLibrary.Validation.ModelValidation
{
    public class StudentList
    {
        [Required(ErrorMessage = "FullName is required.")]
        [IsLetter(ErrorMessage = "FullName is required.")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Date of Birth is required.")]
        public string DOB { get; set; }

        [Required(ErrorMessage = "Gender is required.")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        public string Address { get; set; }
    }
}
