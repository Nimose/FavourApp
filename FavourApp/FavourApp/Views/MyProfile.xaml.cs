using Favourpp.Models;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Favourpp.Helpers;
using Xamarin.Forms.Xaml;
using Favourpp.Services;
namespace Favourpp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyProfile : ContentPage
    {
        ObservableCollection<Service> listItems = new ObservableCollection<Service>();
        User user;
        string facebookId;
        string accessToken;

        public MyProfile()
        {           
            facebookId = Settings.FacebookId;
            accessToken =   Settings.AccessToken;
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            if (facebookId == string.Empty & accessToken == string.Empty)
            {
                await Navigation.PushAsync(new Login());
            }
            else
            {
                var favorService = new FavorService();
                var user = await favorService.GetUserAsync(facebookId);
                this.user = user;               
                UserFname.Text = user.Fname;
                UserLname.Text = user.Lname;
                UserImage.Source = user.Imgurl;
                if (user.Services.Length > 0)
                {
                    ServiceList.ItemsSource = user.Services;
                }
                UserDescription.Text = user.Description;
                Title = user.Fname + " " + user.Lname;
            }

        }

        async void UpdateUser_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PushModalAsync(new UpdateMyProfile(user));
        }

        private async void Logout_Clicked(object sender, System.EventArgs e)
        {
            Settings.ClearEverything();
            await Navigation.PopToRootAsync();
        }
    }
}
