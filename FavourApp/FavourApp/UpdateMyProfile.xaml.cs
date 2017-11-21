using FavourApp.Models;
using FavourApp.Services;
using FavourApp.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FavourApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UpdateMyProfile : ContentPage
    {
       
        ObservableCollection<Service> listItems = new ObservableCollection<Service>();

        public UpdateMyProfile(string fname, string lname, string id)
        {
            BindingContext = new CategoriesViewModel();

            FacebookIdLabel.Text = id;
            FnameLabel.Text = fname;
            LnameLabel.Text = lname;
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            var favorService = new FavorService();
            var user = await favorService.GetUserAsync(FacebookIdLabel.Text);
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
                Facebookid = FacebookIdLabel.Text,
                Imgurl = ImageUrl.ToString(),
                Range = int.Parse(Range.Text),
                Zipcode = Zip.Text,
                Services = listItems.ToList().ToArray()
            };
            favorService.CreateProfileAsync(user);
            await Navigation.PopModalAsync();
        }

        private void AddService_Clicked(object sender, System.EventArgs e)
        {
            Service service = new Service();
            int price = int.Parse(PriceService.Text);
            var selectedValue = PickerService.Items[PickerService.SelectedIndex];

            service.Category = selectedValue;
            service.Price = price;
            listItems.Add(service);

        }

        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}