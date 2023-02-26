using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace StudentAttandanceLibrary.Validation.CustomValidation
{
    public class IsFileAttribute : ValidationAttribute
    {

    }

    public class IsExcelAttribute : ValidationAttribute
    {
        public IsExcelAttribute()
        {
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                var file = value as IFormFile;

                if (file == null || file.Length == 0)
                {
                    return new ValidationResult("Please select a file.");
                }

                if (!file.FileName.EndsWith(".xlsx", StringComparison.OrdinalIgnoreCase))
                {
                    return new ValidationResult("The selected file must be an Excel file (.xlsx).");
                }
            }

            return ValidationResult.Success;
        }
    }
}
