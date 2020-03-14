using System;
using Microsoft.AspNetCore.Http;
using OppJar.Web.CustomValidations;

namespace OppJar.Web.Models
{
    public class AddChildViewModel
    {
        public string Id { get; set; }
        [ChildrenInfo]
        public string FirstName { get; set; }
        [ChildrenInfo]
        public string LastName { get; set; }
        [DateOfBirthChild]
        public DateTime DOB { get; set; } = DateTime.Now;
        public string School { get; set; }
        public string Avatar { get; set; }
        public string Banner { get; set; }
        public string UserType { get; set; } = "Child";
        public IFormFile AvatarFile { get; set; }
        public IFormFile BannerFile { get; set; }
    }
}
