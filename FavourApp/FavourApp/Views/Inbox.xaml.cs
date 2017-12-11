using FavourApp.Models;
using FavourApp.ViewModels;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using FavourApp.Helpers;
using Xamarin.Forms.Xaml;
using FavourApp.Services;

namespace FavourApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Inbox : ContentPage
    {
        //FacebookProfile facebookProfile;
        //private string ClientId = "930931753728262";
        string userId = Settings.FacebookId;
        public Inbox()
        {                 
            InitializeComponent();


        }
        protected override async void OnAppearing()
        {
            if (Settings.FacebookId == "" & Settings.AccessToken == "")
            {
                await Navigation.PushAsync(new MyProfile());
            }
            var favorService = new FavorService();
            ConversationsList.ItemsSource = await favorService.GetConversationsAsync(userId);
            base.OnAppearing();
        }

        private async void ConversationsList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var conversation = e.Item as Conversation;
            var favorService = new FavorService();
            User user = await favorService.GetUserAsync(conversation.Users.GetValue(1).ToString());
            await Navigation.PushAsync(new Message(user, conversation.Id));
        }
    }
}