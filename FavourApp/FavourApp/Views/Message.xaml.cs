using FavourApp.Models;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using FavourApp.Helpers;
using FavourApp.Services;

namespace FavourApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Message : ContentPage
    {
        User User;
        string FacebookId;
        public Message(User user)
        {
            User = user;
            FacebookId = Settings.FacebookId;

            InitializeComponent();
        }

        private void SendMessage_Clicked(object sender, EventArgs e)
        {
            var favorService = new FavorService();
            var message = new FavourApp.Models.Message
            {
                ConversationID = "",
                UserId = User.Facebookid,
                Timestamp = DateTime.Now,
                ConversationMessage = MessageToSend.Text
            };
            favorService.SendMessageAsync(message);
        }
    }
}