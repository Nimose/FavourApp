using Newtonsoft.Json;

namespace FavourApp.Models
{
    public class Category
    {
        [JsonProperty("id")]    
        public string Id { get; set; }
        [JsonProperty("name")]    
        public string Name { get; set; }

    }
}
