using Newtonsoft.Json;

namespace FavourApp.Models
{
    public class Service
    {
        [JsonProperty("category")]
        public string Category { get; set; }
        [JsonProperty("price")]
        public int Price { get; set; }
    }
}
