using OppJar.Common.Enum;
using OppJar.Web.CustomValidations;
using System.ComponentModel.DataAnnotations;

namespace OppJar.Web.Models
{
    public class ParentInfoViewModel
    {
        public UserType UserType { get; set; }
        [RequiredIf("UserType", Operator.EqualTo, UserType.Parent, ErrorMessage = "SSN is required.")]
        [RegularExpression(@"^\d{3} \d{2} \d{4}$", ErrorMessage = "SSN is not valid.")]
        public string SSN { get; set; }
        [RequiredIf("UserType", Operator.EqualTo, UserType.Parent, ErrorMessage = "Primary Address is required.")]
        public string PrimaryAddress { get; set; }
        public string SecondAddress { get; set; }
        [RequiredIf("UserType", Operator.EqualTo, UserType.Parent, ErrorMessage = "City is required.")]
        public string City { get; set; }
        [RequiredIf("UserType", Operator.EqualTo, UserType.Parent, ErrorMessage = "State is required.")]
        public string State { get; set; }
        public string StateValue { get; set; }
        [RequiredIf("UserType", Operator.EqualTo, UserType.Parent, ErrorMessage = "ZipCode is required.")]
        public string ZipCode { get; set; }
    }
}
