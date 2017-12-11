using FavourApp.Models;
using FavourApp.Services;
using System;
using Xamarin.Forms;
using FavourApp.Helpers;
namespace FavourApp
{
    public partial class UserProfile : ContentPage
    {
        User User;

        public string fbid = "";
        public UserProfile(string facebookId)
        {
            this.fbid = facebookId;
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            var favorService = new FavorService();
            var user = await favorService.GetUserAsync(fbid);
            this.User = user;
            UserFname.Text = user.Fname;
            UserLname.Text = user.Lname;
            UserImage.Source = user.Imgurl;
            ServiceList.ItemsSource = user.Services;
            UserDescription.Text = user.Description;
        }

        async void Message_Clicked(object sender, EventArgs e)
        {
            if (Settings.FacebookId == "" & Settings.AccessToken == "")
            {
                await Navigation.PushAsync(new MyProfile());
            }
            var favorService = new FavorService();
            var conversation = await favorService.ReturnConversationAsync(Settings.FacebookId, User.Facebookid);
            await Navigation.PushAsync(new Message(User, conversation.Id));
        }
    }
}