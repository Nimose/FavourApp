using FavourApp.Models;
using FavourApp.ViewModels;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Linq;
using System.Text;

namespace FavourApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MyProfile : ContentPage
	{
        private const string Url = "http://ec2-52-59-154-95.eu-central-1.compute.amazonaws.com:3000/";    
        HttpClient _client = new HttpClient();    
        ObservableCollection<Category.Categories> _categories;
        ObservableCollection<User.Service> listItems = new ObservableCollection<User.Service>();
        //ClientId ligger på docs
        private string ClientId = "930931753728262";

        public MyProfile()
		{
            InitializeComponent();

            var apiRequest =
                "https://www.facebook.com/dialog/oauth?client_id="
                + ClientId
                + "&display=popup"
                + "&response_type=token"
                + "&redirect_uri=https://www.facebook.com/connect/login_success.html";

            var webView = new WebView
            {
                Source = apiRequest,
                HeightRequest = 1
            };

            webView.Navigated += WebViewOnNavigated;

            Content = webView;
        }

        protected override async void OnAppearing()
        {
            var content = await _client.GetStringAsync(Url + "categories/");
            var category = JsonConvert.DeserializeObject<List<Category.Categories>>(content);
            _categories = new ObservableCollection<Category.Categories>(category);
            ListServices.ItemsSource = listItems;

            PickerService.ItemsSource = _categories;
            base.OnAppearing();
        }

        private async void WebViewOnNavigated(object sender, WebNavigatedEventArgs e)
        {

            var accessToken = ExtractAccessTokenFromUrl(e.Url);

            if (accessToken != "")
            {
                var vm = BindingContext as FacebookViewModel;

                await vm.SetFacebookUserProfileAsync(accessToken);

                Content = MainStackLayout;
            }
        }

        private string ExtractAccessTokenFromUrl(string url)
        {
            if (url.Contains("access_token") && url.Contains("&expires_in="))
            {
                var at = url.Replace("https://www.facebook.com/connect/login_success.html#access_token=", "");
                
                var accessToken = at.Remove(at.IndexOf("&expires_in="));

                return accessToken;
            }

            return string.Empty;
        }

        private void AddService_Clicked(object sender, System.EventArgs e)
        {
            User.Service service = new User.Service();
            int price = int.Parse(PriceService.Text);
            var selectedValue = PickerService.Items[PickerService.SelectedIndex];

            service.category =  selectedValue;
            service.price = price;
            listItems.Add(service);

        }

        async void UpdateUser_Clicked(object sender, System.EventArgs e)
        {
            var user = new User.Users {
                description = Description.Text,
                fname = Fname.Text,
                lname = Lname.Text,
                facebookid = FacebookId.Text,
                imgurl = ImageUrl.Source.ToString(),
                range = int.Parse(Range.Text),
                zipcode = Zip.Text,
                services = listItems.ToList().ToArray()
            };

            var content = JsonConvert.SerializeObject(user);
         
            await _client.PostAsync(Url + "user", new StringContent(content, Encoding.UTF8, "application/json"));

        }
    }
}
