using System;
using Newtonsoft.Json;

namespace FavourApp.Models
{
    public class Service
    {
        [JsonProperty("category")]
        public string CategoryName { get; set; }
        [JsonProperty("price")]
        public int Price { get; set; }

        public static implicit operator string(Service v)
        {
            throw new NotImplementedException();
        }

        public static implicit operator Service(string v)
        {
            throw new NotImplementedException();
        }
    }
}
