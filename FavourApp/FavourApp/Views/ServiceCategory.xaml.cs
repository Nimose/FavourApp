using FavourApp.Models;
using FavourApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FavourApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ServiceCategory : ContentPage
    {
        public ServiceCategory(string categoryName)
        {
            BindingContext = new ProfilesViewModel();
            (BindingContext as ProfilesViewModel).GetProfilesWithService(categoryName);
            InitializeComponent();
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