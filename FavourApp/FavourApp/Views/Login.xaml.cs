using Favourpp.Models;
using Favourpp.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Favourpp.Services;
namespace Favourpp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {    
        private string ClientId = "930931753728262";
        string facebookId;
        string accessToken;
        public Login()
        {
            facebookId = Settings.FacebookId;
            accessToken = Settings.AccessToken;          
            InitializeComponent();
        }
        protected async override void OnAppearing()
        {                    
            if (accessToken != string.Empty & facebookId != string.Empty)
            {
                await Navigation.PopToRootAsync();
            }
            else
            {
                var apiRequest =
                "https://www.facebook.com/dialog/oauth?client_id="
                + ClientId
                + "&display=popup"
                + "&response_type=token"
                + "&redirect_uri=https://www.facebook.com/connect/login_success.html";

                var webView = new WebViewCustom()
                {
                    Source = apiRequest,
                    HeightRequest = 1,
                };
                webView.Navigated += WebViewOnNavigated;
                Content = webView;
            }
            base.OnAppearing();
        }
        private async void WebViewOnNavigated(object sender, WebNavigatedEventArgs e)
        {
            var accessToken = ExtractAccessTokenFromUrl(e.Url);
            if (accessToken != "")
            {
                var facebookServices = new FacebookServices();
                var facebookProfile = await facebookServices.GetFacebookProfileAsync(accessToken);

                Settings.FacebookId = facebookProfile.Id;
                string[] arr = new string[] { };

                User user = new User
                {
                    Facebookid = facebookProfile.Id,
                    Fname = facebookProfile.FirstName,
                    Lname = facebookProfile.LastName,
                    Imgurl = facebookProfile.Picture.Data.Url,
                    Description = string.Empty,
                    Range = 0,

                    Zipcode = string.Empty
                };

                var favorService = new FavorService();
                bool check = await favorService.CheckUserAsync(facebookProfile.Id);
                if (check == true)
                {

                    favorService.CreateUserAsync(user);
                }

                await Navigation.PopToRootAsync();
            }
        }
        private string ExtractAccessTokenFromUrl(string url)
        {
            if (url.Contains("access_token") && url.Contains("&expires_in="))
            {
                var at = url.Replace("https://www.facebook.com/connect/login_success.html#access_token=", "");
                var accessToken = at.Remove(at.IndexOf("&expires_in="));
                Settings.AccessToken = accessToken;
                return accessToken;
            }
            return string.Empty;
        }
    }
}