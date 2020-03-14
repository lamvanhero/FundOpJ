using OppJar.Web.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace OppJar.Web.CustomValidations
{
    public class ChildrenInfoAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var model = (AddChildViewModel)validationContext.ObjectInstance;

            if(model.DOB.ToUniversalTime().Date < DateTime.UtcNow.Date)
            {
                if (string.IsNullOrEmpty(model.FirstName)) return new ValidationResult(ErrorMessage);
                
                if (string.IsNullOrEmpty(model.LastName)) return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }
    }
}
