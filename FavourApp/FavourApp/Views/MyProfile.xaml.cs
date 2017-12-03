using FavourApp.Models;
using FavourApp.ViewModels;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using FavourApp.Helpers;
using Xamarin.Forms.Xaml;

namespace FavourApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyProfile : ContentPage
    {     
        ObservableCollection<Service> listItems = new ObservableCollection<Service>();
        FacebookProfile facebookProfile;
        private string ClientId = "930931753728262";

        public MyProfile()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            var accessToken = Settings.AccessToken;
            if (accessToken != "")
            {
                var vm = BindingContext as FacebookViewModel;
                await vm.SetFacebookUserProfileAsync(accessToken);
                facebookProfile = vm.FacebookProfile;
                Content = MainStackLayout;
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
                var vm = BindingContext as FacebookViewModel;
                await vm.SetFacebookUserProfileAsync(accessToken);
                facebookProfile = vm.FacebookProfile;
                Settings.FacebookId = facebookProfile.Id;
                Content = MainStackLayout;
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

        async void UpdateUser_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PushModalAsync(new UpdateMyProfile(facebookProfile));
        }

        private async void Logout_Clicked(object sender, System.EventArgs e)
        {           
            Settings.ClearEverything();
            await Navigation.PopToRootAsync();
        }
    }
}
