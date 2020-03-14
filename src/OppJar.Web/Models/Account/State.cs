using Newtonsoft.Json;

namespace OppJar.Web.Models
{
    public class State
    {
        public string Id { get; set; }
        public string Name { get; set; }
        [JsonProperty("country_id")]
        public string CountryId { get; set; }
        public string Code { get; set; }
    }
}
