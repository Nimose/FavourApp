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
        protected override async void OnAppearing()
        {
            var favorService = new FavorService();
            var user = await favorService.GetUserAsync(facebookProfile.Id);
            if (user != null)
            {
                Zip.Text = user.Zipcode;
                Description.Text = user.Description;
                Range.Text = user.Range.ToString();
                foreach (var item in user.Services)
                {
                    listItems.Add(item);
                }
            }

            ImageUrl.Source = facebookProfile.Picture.Data.Url;
            FnameLabel.Text = facebookProfile.FirstName;
            LnameLabel.Text = facebookProfile.LastName;
            ListServices.ItemsSource = listItems;
            (BindingContext as CategoriesViewModel).GetCategories();
            base.OnAppearing();
        }

        private async void Update_Clicked(object sender, EventArgs e)
        {
            var favorService = new FavorService();
            var user = new User
            {
                Description = Description.Text,
                Fname = FnameLabel.Text,
                Lname = LnameLabel.Text,
                Facebookid = facebookProfile.Id,
                Imgurl = facebookProfile.Picture.Data.Url,
                Range = int.Parse(Range.Text),
                Zipcode = Zip.Text,
                Services = listItems.ToList().ToArray()
            };
            favorService.CreateUserAsync(user);
            await Navigation.PopModalAsync();
        }

        private void AddService_Clicked(object sender, System.EventArgs e)
        {
            int price = int.Parse(PriceService.Text);
            var selectedValue = PickerService.Items[PickerService.SelectedIndex];
            if (selectedValue != null)
            {
                Service service = new Service
                {
                    Category = selectedValue,
                    Price = price
                };
                listItems.Add(service);

                PickerService.SelectedIndex = -1;
                PriceService.Text = "";
            }
        }

        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private void ListServices_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var item = (e.Item as Service);
            PriceService.Text = item.Price.ToString();
            //PickerService.Items[PickerService.SelectedIndex] = item.CategoryName;

        }
        public void OnDelete(object sender, ItemTappedEventArgs e)
        {
            var item = (sender as MenuItem).BindingContext as Service;        
            listItems.Remove(item);
        }

        
    }
}