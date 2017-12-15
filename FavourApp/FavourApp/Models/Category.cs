using Newtonsoft.Json;
namespace Favourpp.Models
{
    public class Category
    {
        [JsonProperty("id")]    
        public string Id { get; set; }
        [JsonProperty("name")]    
        public string Name { get; set; }
    }
}
