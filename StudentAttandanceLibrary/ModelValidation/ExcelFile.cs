using System.ComponentModel.DataAnnotations;

namespace StudentAttandanceLibrary.ModelValidation
{
    public class ExcelFile
    {
        [Required]
        public string File { get; set; }
    }
}
