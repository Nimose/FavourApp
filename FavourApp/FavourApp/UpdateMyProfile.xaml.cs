using FavourApp.Models;
using FavourApp.Services;
using FavourApp.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FavourApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UpdateMyProfile : ContentPage
    {

        ObservableCollection<Service> listItems = new ObservableCollection<Service>();
        FacebookProfile facebookProfile;
        public UpdateMyProfile(FacebookProfile facebookProfile)
        {
            BindingContext = new CategoriesViewModel();
            this.facebookProfile = facebookProfile;
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            ImageUrl.Source = facebookProfile.Picture.Data.Url;
            FacebookIdLabel.Text = facebookProfile.Id;
            FnameLabel.Text = facebookProfile.FirstName;
            LnameLabel.Text = facebookProfile.LastName;
            ListServices.ItemsSource = listItems;
            (BindingContext as CategoriesViewModel).GetCategories();

            base.OnAppearing();
        }

        private async void Update_Clicked(object sender, EventArgs e)
        {
            var user = new User
            {
                Description = Description.Text,
                Fname = FnameLabel.Text,
                Lname = LnameLabel.Text,
                Facebookid = FacebookIdLabel.Text,
                Imgurl = ImageUrl.ToString(),
                Range = int.Parse(Range.Text),
                Zipcode = Zip.Text,
                Services = listItems.ToList().ToArray()
            };

            (BindingContext as ProfilesViewModel).CreateProfile(user);
            await Navigation.PopModalAsync();
        }

        private void AddService_Clicked(object sender, System.EventArgs e)
        {
            int price = int.Parse(PriceService.Text);
            var selectedValue = PickerService.Items[PickerService.SelectedIndex];
            Service service = new Service
            {
                Category = selectedValue,
                Price = price
            };
            listItems.Add(service);
        }

        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}