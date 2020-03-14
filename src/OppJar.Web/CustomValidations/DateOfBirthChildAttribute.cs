using OppJar.Web.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace OppJar.Web.CustomValidations
{
    public class DateOfBirthChildAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var model = (AddChildViewModel)validationContext.ObjectInstance;

            var age = (DateTime.UtcNow - model.DOB).Days / 365.25m;

            if (age > 18) return new ValidationResult("Sorry, the child can not more than 18 years old.");

            return ValidationResult.Success;
        }
    }
}
