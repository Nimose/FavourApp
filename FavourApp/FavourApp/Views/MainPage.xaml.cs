using System;
using Xamarin.Forms;
using FavourApp.Models;
using FavourApp.ViewModels;
using FavourApp.Views;

namespace FavourApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            BindingContext = new ProfilesViewModel();
            (BindingContext as ProfilesViewModel).GetProfiles();
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
        async void ProfileList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var UserObj = (e.Item as User);
            string facebookId = UserObj.Facebookid;
            await Navigation.PushAsync(new UserProfile(facebookId));
        }

        async void Profile_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MyProfile());
        }

        async void  Children_Clicked(object sender, EventArgs e)
        {
            string categoryName = "børnepasning";
            await Navigation.PushAsync(new ServiceCategory(categoryName));
        }
    }
}

