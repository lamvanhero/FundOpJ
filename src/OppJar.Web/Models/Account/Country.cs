using Newtonsoft.Json;

namespace OppJar.Web.Models
{
    public class Country
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("sortname")]
        public string SortName { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("phoneCode")]
        public int PhoneCode { get; set; }
    }
}
