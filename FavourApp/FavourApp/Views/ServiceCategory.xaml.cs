using Favourpp.Models;
using Favourpp.Services;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
namespace Favourpp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ServiceCategory : ContentPage
    {
        string categoryName;
        public ServiceCategory(string categoryName)
        {
            Title = categoryName.ToUpper();
            this.categoryName = categoryName;
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            var favorService = new FavorService();
            ProfileList.ItemsSource = await favorService.GetUsersCategoryAsync(categoryName);
            base.OnAppearing();
        }
        async void ProfileList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var user = (e.Item as User);          
            await Navigation.PushAsync(new UserProfile(user));
        }

        #region Category buttons      
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
        #endregion

        #region Profile and home buttons
        async void Profile_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MyProfile());
        }
        async void Home_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopToRootAsync();
        }
        async void Inbox_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Inbox());
        }
        #endregion
    }
}