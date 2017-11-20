using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using FavourApp.Models;
using System.Net.Http;
using System.Collections.ObjectModel;

namespace FavourApp.Services
{
  public  class FavorAppService
    {
        private const string UsersUrl = "http://ec2-52-59-154-95.eu-central-1.compute.amazonaws.com:3000/user";
        private HttpClient  _client = new HttpClient();
        private ObservableCollection<Rootobject> _users;

        public class Rootobject
        {
            public Class1[] Property1 { get; set; }
        }

        public class Class1
        {
            public string _id { get; set; }
            public string facebookid { get; set; }
            public string description { get; set; }
            public int range { get; set; }
            public string zipcode { get; set; }
            public int __v { get; set; }
            public Service[] services { get; set; }
        }

        public class Service
        {
            public string category { get; set; }
            public int price { get; set; }
        }

        //public async Task<User> GetUsers()
        //{
        //    var content = await _client.GetStringAsync(UsersUrl;
        //    JsonConvert.DeserializeObject<List<Rootobject>>(content);

          

        //}
    }
}
