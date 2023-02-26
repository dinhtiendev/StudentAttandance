using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace StudentAttandanceLibrary.Validation.CustomValidation
{
    public class IsStringAttribute
    {
    }

    public class IsLetterAttribute : ValidationAttribute
    {
        public IsLetterAttribute()
        {
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                var s = value as string;

                if (!Regex.IsMatch(s, @"^[a-zA-Z ]+$"))
                {
                    return new ValidationResult("Input must is letter.");
                }
            }

            return ValidationResult.Success;
        }
    }

    public class IsSamePassword : ValidationAttribute
    {
        private readonly string _passwordPropertyName;

        public IsSamePassword(string passwordPropertyName)
        {
            _passwordPropertyName = passwordPropertyName;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var passwordProperty = validationContext.ObjectType.GetProperty(_passwordPropertyName);
            if (passwordProperty == null)
            {
                return new ValidationResult($"Unknown property {_passwordPropertyName}");
            }

            var passwordValue = passwordProperty.GetValue(validationContext.ObjectInstance, null) as string;
            var confirmationValue = value as string;

            if (passwordValue != confirmationValue)
            {
                return new ValidationResult("The password and confirmation password do not match.");
            }

            return ValidationResult.Success;
        }
    }
}
