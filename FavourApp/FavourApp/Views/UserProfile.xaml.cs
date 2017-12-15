using Favourpp.Models;
using Favourpp.Services;
using System;
using Xamarin.Forms;
using Favourpp.Helpers;
using Xamarin.Forms.Xaml;

namespace Favourpp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserProfile : ContentPage
    {
        User user;
        string facebookId;
        public UserProfile(User user)
        {
            Title = user.Fname + " " + user.Lname;
            this.user = user;
            this.facebookId = Settings.FacebookId;
            InitializeComponent();
        }
        protected override void OnAppearing()
        {    
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
            var conversation = await favorService.ReturnConversationAsync(facebookId, user.Facebookid);
            await Navigation.PushAsync(new Message(user, conversation.Id));
        }
    }
}