using Favourpp.Models;
using Xamarin.Forms;
using Favourpp.Helpers;
using Xamarin.Forms.Xaml;
using Favourpp.Services;

namespace Favourpp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Inbox : ContentPage
    {
        string facebookId;
        string accessToken;
        public Inbox()
        {
            facebookId = Settings.FacebookId;
            accessToken = Settings.AccessToken;
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            if (facebookId== string.Empty & accessToken == string.Empty)
            {
                await Navigation.PushAsync(new Login());
            }
            else
            {
                var favorService = new FavorService();
                ConversationsList.ItemsSource = await favorService.GetConversationsAsync(facebookId);
                base.OnAppearing();
            }
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