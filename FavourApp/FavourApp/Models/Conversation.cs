using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FavourApp.Models
{
    public class Conversation
    {
        [JsonProperty("_id")]
        public string Id { get; set; }

        [JsonProperty("user")]
        public string[] Users { get; set; }
    }
}
