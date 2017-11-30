using FavourApp.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text;

namespace FavourApp.Services
{
    public class FavorService
    {
        private const string Url = "http://ec2-52-59-154-95.eu-central-1.compute.amazonaws.com:3000/";
        private const string ApiKey = "?token=eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJhZG1pbiI6dHJ1ZSwiaWF0IjoxNTExMTg0MzEzfQ.weCro9I2mFufR2zthIJK-27BuJuV4bLKzSWpK3LrMjQ";

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

        public async Task<List<User>> GetUsersCategoryAsync()
        {

            //string categoryName = "dyrepasning";
            var requestUrl = Url + "user/services/børnepasning" + ApiKey;
            HttpClient _client = new HttpClient();

            var usersJson = await _client.GetStringAsync(requestUrl);
            var users = JsonConvert.DeserializeObject<List<User>>(usersJson);
            return users;
        }

 
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
        public async void CreateProfileAsync(User user)
        {
            var postUrl = Url + "user" + ApiKey;
            HttpClient _client = new HttpClient();
            var userJson = JsonConvert.SerializeObject(user);
            await _client.PostAsync(postUrl, new StringContent(userJson, Encoding.UTF8, "application/json"));
        }
        public async void UpdateProfileAsync(User user)
        {
            var postUrl = Url + "user/" + user.Facebookid + ApiKey;
            HttpClient _client = new HttpClient();
            var userJson = JsonConvert.SerializeObject(user);
            await _client.PutAsync(postUrl, new StringContent(userJson, Encoding.UTF8, "application/json"));
        }
        public bool CheckUser(string facebookId)
        {
            var user = GetUserAsync(facebookId);
            if (user == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<Conversation> GetConversation(string userId, string conversationId)
        {
            var requestUrl = Url + "user/" + userId + "conversation/" + conversationId + ApiKey;
            HttpClient _client = new HttpClient();
            var conversationJson = await _client.GetStringAsync(requestUrl);
            var conversation = JsonConvert.DeserializeObject<Conversation>(conversationJson);
            return conversation;
        }
        public async Task<List<Conversation>> GetUserConversations(string userId)
        {
            var reguestUrl = Url + "user/" + userId + "/conversations/" + ApiKey;
            HttpClient _client = new HttpClient();
            var user = GetUserAsync(userId);
            var userConversationsJson = await _client.GetStringAsync(reguestUrl);
            var userConversations = JsonConvert.DeserializeObject<List<Conversation>>(userConversationsJson);
            return userConversations;
        }
        public async void PostMessage(string userId, string conversationId, ConversationMessage conversationMessage)
        {
            var postUrl = Url + "user/" + userId + "conversation/" + conversationId + ApiKey;
            HttpClient _client = new HttpClient();
            var conversationMessageJson = JsonConvert.SerializeObject(conversationMessage);
            await _client.PostAsync(postUrl, new StringContent(conversationMessageJson, Encoding.UTF8, "application/json"));
        }
    }

}
