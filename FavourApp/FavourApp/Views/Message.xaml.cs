using Favourpp.Models;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Favourpp.Helpers;
using Favourpp.Services;
using System.Collections.ObjectModel;
namespace Favourpp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Message : ContentPage
    {
        User user;
        string facebookId;
        string accessToken;
        string conversationId;
        ObservableCollection<Models.Message> conversationMessages = new ObservableCollection<Models.Message>();
        public Message(User user, string conversationId)
        {           
            this.user = user;
            this.conversationId = conversationId;
            facebookId = Settings.FacebookId;
            accessToken = Settings.AccessToken;
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            if (facebookId == string.Empty & accessToken == string.Empty)
            {
                await Navigation.PushAsync(new Login());
            }
            else
            {
                var favorService = new FavorService();
                var id = favorService.CleanId(conversationId);
                var messages = await favorService.GetMessagesAsync(id);
                foreach (var message in messages)
                {
                    conversationMessages.Add(message);
                }
                ConversationList.ItemsSource = conversationMessages;
                base.OnAppearing();
            }
        }

        private void SendMessage_Clicked(object sender, EventArgs e)
        {
            var favorService = new FavorService();
            var message = new Models.Message
            {
                ConversationID = conversationId,
                UserId = facebookId,
                Timestamp = DateTime.Now,
                ConversationMessage = MessageToSend.Text
            };
            conversationMessages.Add(message);
            MessageToSend.Text = "";
            favorService.SendMessageAsync(message);
        }
    }
}