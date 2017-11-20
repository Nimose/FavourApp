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
        ObservableCollection<Category> _categories;
        ObservableCollection<Service> listItems = new ObservableCollection<Service>();
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
            var category = JsonConvert.DeserializeObject<List<Category>>(content);
            var user = await _client.GetStringAsync(Url + "user/" + FacebookIdLabel.Text);
            _categories = new ObservableCollection<Category>(category);          
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

      

        async void UpdateUser_Clicked(object sender, System.EventArgs e)
        {            
            await Navigation.PushModalAsync(new UpdateMyProfile());            
        }
    }
}
