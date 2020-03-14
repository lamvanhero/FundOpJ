using OppJar.Web.Models;
using System;
using System.ComponentModel.DataAnnotations;
using OppJar.Common.Enum;

namespace OppJar.Web.CustomValidations
{
    public class DateOfBirthAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var model = (RegisterViewModel)validationContext.ObjectInstance;
            
            var age = (DateTime.UtcNow - model.DOB.Value.ToUniversalTime()).Days / 365.25m;

            if (age < 18 && model.UserType == UserType.Parent) return new ValidationResult("Sorry, only users over 18 may be allowed to register.");

            return ValidationResult.Success;
        }
    }
}
