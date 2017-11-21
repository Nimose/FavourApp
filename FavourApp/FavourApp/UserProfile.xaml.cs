using FavourApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FavourApp
{
    
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class UserProfile : ContentPage
	{
        public string fbid = "";
        private const string Url = "http://ec2-52-59-154-95.eu-central-1.compute.amazonaws.com:3000/";
        HttpClient _client = new HttpClient();

        public UserProfile (string facebookId)
		{
            this.fbid = facebookId;
			InitializeComponent ();
        }

        protected override async void OnAppearing()
        {
            var content = await _client.GetStringAsync(Url + "user/" + fbid);
            var user = JsonConvert.DeserializeObject<User>(content);
            UserFname.Text = user.Fname;
            UserLname.Text = user.Fname;
            UserImage.Source = user.Imgurl;
            ServiceList.ItemsSource = user.Services;
            UserDescription.Text = user.Description;
        }

        async void Message_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Message());
        }
    }
}