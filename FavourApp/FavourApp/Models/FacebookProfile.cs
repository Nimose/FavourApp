using Newtonsoft.Json;
namespace Favourpp.Models
{
    public class FacebookProfile
    {
        public Picture Picture { get; set; }
        [JsonProperty("first_name")]
        public string FirstName { get; set; }
        [JsonProperty("last_name")]
        public string LastName { get; set; }
        public string Id { get; set; }
    }
    public class Picture
    {
        public Data Data { get; set; }
    }
    public class Data
    {
        public bool IsSilhouette { get; set; }
        public string Url { get; set; }
    }
}
