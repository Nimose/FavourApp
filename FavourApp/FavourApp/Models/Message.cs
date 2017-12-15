using Newtonsoft.Json;
using System;
namespace Favourpp.Models
{
    public class Message
    {    
        [JsonProperty("message")]
        public string ConversationMessage { get; set; }
        [JsonProperty("user")]
        public string UserId { get; set; }
        [JsonProperty("conversationID")]
        public string ConversationID { get; set; }
        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; set; }
    }
}
