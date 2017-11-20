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

    public partial class MainPage : ContentPage
    {
        private const string Url = "http://ec2-52-59-154-95.eu-central-1.compute.amazonaws.com:3000/";

        HttpClient _client = new HttpClient();
        ObservableCollection<User> _users;


        public MainPage()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            try
            {
                var content = await _client.GetStringAsync(Url + "user/");
                var user = JsonConvert.DeserializeObject<List<User>>(content);
                _users = new ObservableCollection<User>(user);

                List<UserTest> list = new List<UserTest>();
                foreach (var u in _users)
                {
                    //Check if users provides any services 
                    if (u.Services.Length.Equals(0)) { }
                    else
                    {
                        list.Add(new UserTest { Facebookid = Int32.Parse(u.Facebookid), Fname = u.Fname, Lname = u.Lname });
                    }
                }
                postsListView.ItemsSource = list;
                base.OnAppearing();
            }
            catch (Exception e)
            {

                Debug.WriteLine(e);
            }
               

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
        await Navigation.PushAsync(new MyProfile());
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


    async void ViewProfile_Clicked(object sender, EventArgs e)
    {
        var facebookid = 20323023;
        await Navigation.PushAsync(new UserProfile(facebookid));
    }
}

public class UserTest
{
    public int Facebookid { get; set; }
    public string Fname { get; set; }
    public string Lname { get; set; }

}
}

