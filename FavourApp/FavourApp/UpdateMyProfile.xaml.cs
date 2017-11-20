using FavourApp.Models;
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
        private const string Url = "http://ec2-52-59-154-95.eu-central-1.compute.amazonaws.com:3000/";
        HttpClient _client = new HttpClient();
        ObservableCollection<Category> _categories;
        ObservableCollection<Service> listItems = new ObservableCollection<Service>();
        private string ApiKey = "?token=eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJhZG1pbiI6dHJ1ZSwiaWF0IjoxNTExMTg0MzEzfQ.weCro9I2mFufR2zthIJK-27BuJuV4bLKzSWpK3LrMjQ";

        public UpdateMyProfile()
		{
			InitializeComponent ();
		}
        protected override async void OnAppearing()
        {
            var content = await _client.GetStringAsync(Url + "categories/");
            var category = JsonConvert.DeserializeObject<List<Category>>(content);
            var user = await _client.GetStringAsync(Url + "user/" + FacebookIdLabel.Text);
            _categories = new ObservableCollection<Category>(category);
            ListServices.ItemsSource = listItems;
            PickerService.ItemsSource = _categories;
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
            var content = JsonConvert.SerializeObject(user);
            await _client.PostAsync(Url + "user/" + ApiKey, new StringContent(content, Encoding.UTF8, "application/json"));
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