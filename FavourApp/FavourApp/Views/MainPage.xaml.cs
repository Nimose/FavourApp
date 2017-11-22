using System;
using System.Collections.Generic;
using Xamarin.Forms;
using FavourApp.Models;
using System.Collections.ObjectModel;
using System.Net.Http;
using Newtonsoft.Json;
using System.Diagnostics;
using FavourApp.ViewModels;
using FavourApp.Services;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace FavourApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            BindingContext = new ProfilesViewModel();
            InitializeComponent();
        }
        protected override void OnAppearing()
        {        
                (BindingContext as ProfilesViewModel).GetProfilesWithServices();
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
    }

}

