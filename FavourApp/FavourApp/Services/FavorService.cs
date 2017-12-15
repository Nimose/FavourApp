using Favourpp.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text;
using System;

namespace Favourpp.Services
{
    public class FavorService
    {
        private const string Url = "http://ec2-52-59-154-95.eu-central-1.compute.amazonaws.com:3000/";
        private const string ApiKey = "?token=eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJhZG1pbiI6dHJ1ZSwiaWF0IjoxNTExMTg0MzEzfQ.weCro9I2mFufR2zthIJK-27BuJuV4bLKzSWpK3LrMjQ";

        #region User CRUD
        public async Task<User> GetUserAsync(string facebookId)
        {
            var requestUrl = Url + "user/" + facebookId + ApiKey;
            HttpClient _client = new HttpClient();
            var userJson = await _client.GetStringAsync(requestUrl);
            var user = JsonConvert.DeserializeObject<User>(userJson);
            return user;
        }
        public async Task<List<User>> GetUsersAsync()
        {
            var requestUrl = Url + "user" + ApiKey;
            HttpClient _client = new HttpClient();

            var usersJson = await _client.GetStringAsync(requestUrl);
            var users = JsonConvert.DeserializeObject<List<User>>(usersJson);
            return users;
        }
        public async Task<List<User>> GetUsersWithServicesAsync()
        {
            var users = await GetUsersAsync();
            List<User> usersWithServices = new List<User>();
            foreach (var user in users)
            {
                if (user.Services.Length.Equals(0) || user.Services.Equals(null)) { }
                else
                {
                    usersWithServices.Add(new User
                    {
                        Facebookid = user.Facebookid,
                        Fname = user.Fname,
                        Lname = user.Lname,
                        Description = user.Description,
                        Imgurl = user.Imgurl,
                        Range = user.Range,
                        Zipcode = user.Zipcode,
                        Services = user.Services
                    });
                }
            }
            return usersWithServices;
        }
        public async Task<List<User>> GetUsersCategoryAsync(string categoryName)
        {
            var requestUrl = Url + "user/services/" + categoryName + ApiKey;
            HttpClient _client = new HttpClient();
            var usersJson = await _client.GetStringAsync(requestUrl);
            var users = JsonConvert.DeserializeObject<List<User>>(usersJson);
            return users;
        }
        public async void CreateUserAsync(User user)
        {
            var postUrl = Url + "user" + ApiKey;
            HttpClient _client = new HttpClient();
            var userJson = JsonConvert.SerializeObject(user);
            await _client.PostAsync(postUrl, new StringContent(userJson, Encoding.UTF8, "application/json"));
        }
        public async void UpdateUserAsync(User user)
        {
            var postUrl = Url + "user/" + user.Facebookid + ApiKey;
            HttpClient _client = new HttpClient();
            var userJson = JsonConvert.SerializeObject(user);
            await _client.PutAsync(postUrl, new StringContent(userJson, Encoding.UTF8, "application/json"));
        }
        #endregion

        #region Categories and services
        public async Task<List<Category>> GetCategoriesAsync()
        {
            var requestUrl = Url + "categories" + ApiKey;
            HttpClient _client = new HttpClient();
            var categoriesJson = await _client.GetStringAsync(requestUrl);
            var categories = JsonConvert.DeserializeObject<List<Category>>(categoriesJson);
            return categories;
        }
        public async Task<List<Models.Service>> GetServicesAsync()
        {
            var requestUrl = Url + "user" + ApiKey;
            HttpClient _client = new HttpClient();
            var servicesJson = await _client.GetStringAsync(requestUrl);
            var services = JsonConvert.DeserializeObject<List<Models.Service>>(servicesJson);
            return services;
        }
        #endregion

        #region Conversation and messages    
        public async Task<List<Conversation>> GetConversationsAsync(string userId)
        {
            var requestUrl = Url + "conversation/user/" + userId + ApiKey;
            HttpClient _client = new HttpClient();
            var conversationsJson = await _client.GetStringAsync(requestUrl);
            var conversations = JsonConvert.DeserializeObject<List<Conversation>>(conversationsJson);
            return conversations;
        }
        public async Task<Conversation> GetConversationAsync(string conversationId)
        {
            var correctId = CleanId(conversationId);
            var reguestUrl = Url + "conversation/" + correctId + ApiKey;
            HttpClient _client = new HttpClient();
            var conversationJson = await _client.GetStringAsync(reguestUrl);
            var conversation = JsonConvert.DeserializeObject<Conversation>(conversationJson);
            return conversation;
        }
        public async Task<string> CreateConversationAsync(string userIdOne, string recipient)
        {
            string[] userIds = new string[] { userIdOne, recipient };
            Rootobject list = new Rootobject
            {
                User = userIds
            };
            var postUrl = Url + "conversation" + ApiKey;
            HttpClient _client = new HttpClient();
            var conversationJson = JsonConvert.SerializeObject(list);
            var response = await _client.PostAsync(postUrl, new StringContent(conversationJson, Encoding.UTF8, "application/json"));
            var responseContent = "";
            if (response.Content != null)
            {
                responseContent = await response.Content.ReadAsStringAsync();
            };
            return (string)responseContent;
        }
        public class Rootobject
        {
            [JsonProperty("user")]
            public string[] User { get; set; }
        }
        public async void SendMessageAsync(Models.Message message)
        {
            var postUrl = Url + "conversation/message" + ApiKey;
            HttpClient _client = new HttpClient();
            var messageJson = JsonConvert.SerializeObject(message);
            await _client.PostAsync(postUrl, new StringContent(messageJson, Encoding.UTF8, "application/json"));
        }
        public async Task<List<Models.Message>> GetMessagesAsync(string conversationId)
        {
            var requestUrl = Url + "conversation/message/" + conversationId + ApiKey;
            HttpClient _client = new HttpClient();
            var messagesJson = await _client.GetStringAsync(requestUrl);
            var messages = JsonConvert.DeserializeObject<List<Models.Message>>(messagesJson);
            return messages;
        }
        public async Task<Conversation> ReturnConversationAsync(string userId, string recipient)
        {
            string conversationId = string.Empty;
            List<Conversation> conversations = await GetConversationsAsync(userId);
            if (conversations.Count > 0)
            {
                foreach (var convo in conversations)
                {
                    foreach (var user in convo.Users)
                    {
                        if (user == recipient)
                        {
                            conversationId = convo.Id;
                        }
                        else
                        {
                            conversationId = await CreateConversationAsync(userId, recipient);
                        }
                    }
                }
            }
            else
            {
                conversationId = await CreateConversationAsync(userId, recipient);
            }
            Conversation conversation = await GetConversationAsync(conversationId);
            return conversation;
        }         
        #endregion

        #region Helper methods
        public async Task<Boolean> CheckUserAsync(string facebookId)
        {
            User user = await GetUserAsync(facebookId);
            if (user == null)
            {
                return true;
            }
            return false;
        }

        public string CleanId(string conversationId)
        {
            var correctId = conversationId.Replace(@"\", string.Empty).Replace("\"", string.Empty);
            return correctId;
        }
        #endregion
    }
}
