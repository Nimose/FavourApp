using FavourApp.Models;
using FavourApp.Services;
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
            Title = categoryName.ToUpper();
            catName = categoryName;
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            var favorService = new FavorService();
            ProfileList.ItemsSource = await favorService.GetUsersCategoryAsync(catName);
            base.OnAppearing();
        }

        #region Buttons 
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
        async void Children_Clicked(object sender, EventArgs e)
        {
            string categoryName = "børnepasning";
            await Navigation.PushAsync(new ServiceCategory(categoryName));
        }
        async void Garden_Clicked(object sender, EventArgs e)
        {
            string categoryName = "havearvejde";
            await Navigation.PushAsync(new ServiceCategory(categoryName));

        }
        async void Shopping_Clicked(object sender, EventArgs e)
        {
            string categoryName = "indkøb";
            await Navigation.PushAsync(new ServiceCategory(categoryName));
        }
        async void Travel_Clicked(object sender, EventArgs e)
        {
            string categoryName = "transport";
            await Navigation.PushAsync(new ServiceCategory(categoryName));
        }
        async void Home_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopToRootAsync();
        }
        #endregion
    }
}