using Favourpp.Models;
using Favourpp.Services;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;
using Favourpp.Helpers;
using Xamarin.Forms.Xaml;
namespace Favourpp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UpdateMyProfile : ContentPage
    {
        ObservableCollection<Service> listItems = new ObservableCollection<Service>();
        User user;
        string facebookId;
        string accessToken;
        public UpdateMyProfile(User user)
        {
            this.user = user;
            facebookId = Settings.FacebookId;
            accessToken = Settings.AccessToken;
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            if (facebookId == string.Empty & accessToken == string.Empty)
            {
                await Navigation.PushAsync(new Login());
            }
            else
            {
                var favorService = new FavorService();
                if (user != null)
                {
                    Zip.Text = user.Zipcode;
                    Description.Text = user.Description;
                    Range.Text = user.Range.ToString();
                    if (user.Services != null)
                    {
                        foreach (var item in user.Services)
                        {
                            listItems.Add(item);
                        }
                    }
                }
                ImageUrl.Source = user.Imgurl;
                FnameLabel.Text = user.Fname;
                LnameLabel.Text = user.Lname;
                ListServices.ItemsSource = listItems;
                PickerService.ItemsSource = await favorService.GetCategoriesAsync();
                base.OnAppearing();
            }
        }
        private async void Update_Clicked(object sender, EventArgs e)
        {
            var favorService = new FavorService();
            var user = new User
            {
                Description = Description.Text,
                Fname = FnameLabel.Text,
                Lname = LnameLabel.Text,
                Facebookid = facebookId,
                Imgurl = this.user.Imgurl,
                Range = int.Parse(Range.Text),
                Zipcode = Zip.Text,
                Services = listItems.ToList().ToArray()
            };
            favorService.UpdateUserAsync(user);
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
        }
        public void OnDelete(object sender, ItemTappedEventArgs e)
        {
            var item = (sender as MenuItem).BindingContext as Service;
            listItems.Remove(item);
        }
    }
}