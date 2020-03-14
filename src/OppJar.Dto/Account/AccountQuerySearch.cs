using OppJar.Common.Enum;
using System.Text.Json.Serialization;

namespace OppJar.Dto
{
    public class AccountQuerySearch : QuerySearchDefault
    {
        public UserType UserType { get; set; }
        [JsonIgnore]
        public bool IsOwner { get; set; } = false;
    }
}
