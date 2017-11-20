using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FavourApp.Models;
using System.Net.Http;
using Newtonsoft.Json;

namespace FavourApp.Services
{
    public class RestService : IRestService
    {
        HttpClient client;
        private const string ApiUrl = "http://ec2-52-59-154-95.eu-central-1.compute.amazonaws.com:3000/";
        public List<User.Rootobject> Users { get; set; }

        public RestService()
        {
            client = new HttpClient();

            //The HttpClient.MaxResponseContentBufferSize property is used to specify the maximum number of bytes to buffer when reading the content in the HTTP response message. 
            //The default size of this property is the maximum size of an integer. 
            //Therefore, the property is set to a smaller value, as a safeguard, 
            //in order to limit the amount of data that the application will accept as a response from the web service.
            client.MaxResponseContentBufferSize = 256000;

        }

        public async Task<List<User.Rootobject>> GetUsers()
        {        
            Users = new List<User.Rootobject>();
            string usersUrl = ApiUrl + "user";        
            var uri = new Uri(string.Format(usersUrl, string.Empty));

            var response = await client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                Users = JsonConvert.DeserializeObject<List<User.Rootobject>>(content);
            }
            return Users;
        }
    }
}
