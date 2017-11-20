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
        public int fbid = 0;
        private const string Url = "http://ec2-52-59-154-95.eu-central-1.compute.amazonaws.com:3000/";
        HttpClient _client = new HttpClient();
        
        public UserProfile (int facebookid)
		{
            this.fbid = facebookid;
			InitializeComponent ();

        }

        protected override async void OnAppearing()
        {
            var content = await _client.GetStringAsync(Url + "user/" + fbid);
            var user = JsonConvert.DeserializeObject<User.Users>(content);
            UserFname.Text = user.fname;
            UserLname.Text = user.fname;
            UserImage.Source = user.imgurl;
            UserDescription.Text = user.description;
        }

        async void Message_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Message());
        }
    }
}