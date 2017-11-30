using System;
using Newtonsoft.Json;

namespace FavourApp.Models
{
    public class Service
    {
        [JsonProperty("price")]
        public int Price { get; set; }

        [JsonProperty("category")]
        public string Category { get; set; }
    
       
    }
}
