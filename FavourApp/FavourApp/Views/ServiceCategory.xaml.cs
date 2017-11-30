using FavourApp.Models;
using FavourApp.Services;
using FavourApp.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FavourApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ServiceCategory : ContentPage
    {
        string catName;
        public ServiceCategory(string categoryName)
        {
            catName = categoryName;
           
            //BindingContext = new ProfilesViewModel();
            //(BindingContext as ProfilesViewModel).GetProfilesWithService(categoryName);
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            try
            {
                var favorService = new FavorService();
                ProfileList.ItemsSource = await favorService.GetProfilesWithCategories(catName);
            }
            catch (Exception e)
            {

                throw;
            }
          
            base.OnAppearing();
        }
        async void Profile_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MyProfile());
        }
        async void ProfileList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var UserObj = (e.Item as User);
            string facebookId = UserObj.Facebookid;
            await Navigation.PushAsync(new UserProfile(facebookId));
        }
    }
}