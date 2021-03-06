﻿using System;
using System.ComponentModel.DataAnnotations;
using OppJar.Web.CustomValidations;

namespace OppJar.Web.Models
{
    public class RegisterViewModel : ParentInfoViewModel
    {
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is required.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
            ErrorMessage = "Minimum eight characters, at least one uppercase letter, one lowercase letter, one number and one special character(@,$,!,%,*,?,&).")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Confirm password is required.")]
        [Compare("Password", ErrorMessage = "Confirm password doesn't match.")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "First Name is required.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Date of birth is required.")]
        [DataType(DataType.Date)]
        [DateOfBirth()]
        public DateTime? DOB { get; set; } = null;
    }
}
