using Newtonsoft.Json;

namespace FavourApp.Models
{
    public class User
    {
        [JsonProperty("facebookid")]
        public string Facebookid { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("range")]
        public int Range { get; set; }

        [JsonProperty("zipcode")]
        public string Zipcode { get; set; }

        [JsonProperty("fname")]    
        public string Fname { get; set; }

        [JsonProperty("lname")]
        public string Lname { get; set; }

        [JsonProperty("imgurl")]
        public string Imgurl { get; set; }

        [JsonProperty("services")]
        public Service[] Services { get; set; }

    }
}
