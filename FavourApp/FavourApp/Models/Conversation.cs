using Newtonsoft.Json;
namespace Favourpp.Models
{
    public class Conversation
    {
        [JsonProperty("_id")]
        public string Id { get; set; }
        [JsonProperty("user")]
        public string[] Users { get; set; }
    }
}
