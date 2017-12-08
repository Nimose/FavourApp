using FavourApp.Models;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using FavourApp.Helpers;
using FavourApp.Services;
using System.Collections.ObjectModel;

namespace FavourApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Message : ContentPage
    {
        User User;
        string FacebookId;
        string ConversationId;
        ObservableCollection<Models.Message> conversationMessages = new ObservableCollection<Models.Message>();
        public Message(User user, string conversationId)
        {
            var correctId = conversationId.Replace(@"\", string.Empty).Replace("\"", string.Empty);

            User = user;
            ConversationId = correctId;
            FacebookId = Settings.FacebookId;
     
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            try
            {
                var favorService = new FavorService();
                var messages = await favorService.GetMessagesAsync(ConversationId);

                foreach (var message in messages)
                {
                    conversationMessages.Add(message);
                }
                ConversationList.ItemsSource = conversationMessages;
                base.OnAppearing();
            }
            catch (Exception e)
            {

                throw;
            }
        
        }

        private void SendMessage_Clicked(object sender, EventArgs e)
        {
            var favorService = new FavorService();
            var message = new FavourApp.Models.Message
            {
                ConversationID = ConversationId,
                UserId = User.Facebookid,
                Timestamp = DateTime.Now,
                ConversationMessage = MessageToSend.Text
            };
            conversationMessages.Add(message);
            MessageToSend.Text = "";
            favorService.SendMessageAsync(message);
        }
    }
}