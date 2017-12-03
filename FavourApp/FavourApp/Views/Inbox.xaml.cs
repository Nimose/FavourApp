using FavourApp.Models;
using FavourApp.ViewModels;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using FavourApp.Helpers;
using Xamarin.Forms.Xaml;


namespace FavourApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Inbox : ContentPage
    {
        FacebookProfile facebookProfile;
        private string ClientId = "930931753728262";
        public Inbox()
        {
            BindingContext = new ConversationViewModel();
            (BindingContext as ConversationViewModel).GetConversations(facebookProfile.Id);
            InitializeComponent();
        }
   
    }
}