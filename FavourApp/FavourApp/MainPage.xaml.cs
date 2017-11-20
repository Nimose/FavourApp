using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using FavourApp.Services;
using FavourApp.Models;
using System.Collections.ObjectModel;
using System.Net.Http;
using Newtonsoft.Json;
using System.Diagnostics;

namespace FavourApp
{

    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
    }
    public class ProfileTest
    {
        public int Id { get; set; }
        public string Description { get; set; }
    }

    public partial class MainPage : ContentPage
    {
        private const string Url = "http://ec2-52-59-154-95.eu-central-1.compute.amazonaws.com:3000/";
        //private const string Url = "https://jsonplaceholder.typicode.com/posts/";

        HttpClient _client = new HttpClient();
        ObservableCollection<User.Rootobject> _users;
        ObservableCollection<Post> _posts;
        ObservableCollection<Category.Rootobject> _categories;


        public MainPage()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            var content = await _client.GetStringAsync(Url + "user");
            var user = JsonConvert.DeserializeObject<List<User.Rootobject>>(content);
            _users = new ObservableCollection<User.Rootobject>(user);
            List<ProfileTest> list = new List<ProfileTest>();

            postsListView.ItemsSource = _users;
            base.OnAppearing();
        }

        private void Map_Clicked(object sender, EventArgs e)
        {

        }

        private void Search_Clicked(object sender, EventArgs e)
        {

        }

        private void Home_Clicked(object sender, EventArgs e)
        {
        }

        private void Following_Clicked(object sender, EventArgs e)
        {

        }

        private void Inbox_Clicked(object sender, EventArgs e)
        {

        }

        async void Profile_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Profile());
        }
        private void Children_Clicked(object sender, EventArgs e)
        {

        }
        private void Garden_Clicked(object sender, EventArgs e)
        {

        }
        private void Shopping_Clicked(object sender, EventArgs e)
        {

        }
        private void Travel_Clicked(object sender, EventArgs e)
        {

        }
        private void UserList_ItemTapped(object sender, ItemTappedEventArgs e)
        {

        }
    }

}

