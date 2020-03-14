using Newtonsoft.Json;

namespace OppJar.WebApi.Models
{
    public class City
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("state_id")]
        public string StateId { get; set; }
    }
}
