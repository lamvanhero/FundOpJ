using OppJar.Common.Enum;
using System;
using System.Collections.Generic;

namespace OppJar.Web.Models
{
    public class ProfileViewModel
    {
        public string Id { get; set; }
        public string Avatar { get; set; }
        public string Banner { get; set; }
        public string Location { get; set; }
        public string School { get; set; }
        public string About { get; set; }
        public decimal TotalBalance { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Slug { get; set; }
        public Privacy Privacy { get; set; }
        public DateTime DOB { get; set; }
        public string Parent1 { get; set; }
        public string Parent2 { get; set; }
        public int TotalPage { get; set; }
        public List<MemoryViewModel> Memories { get; set; } = new List<MemoryViewModel>();
    }
}
