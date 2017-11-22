using FavourApp.Models;
using FavourApp.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FavourApp.ViewModels
{
    public class ProfilesViewModel
    {
        public ObservableCollection<User> Profiles { get; private set; } = new ObservableCollection<User>();
        public ObservableCollection<User> ProfilesWithServices { get; private set; } = new ObservableCollection<User>();

        public async void GetProfilesWithServices()
        {
            var favorService = new FavorService();
            var users = favorService.GetUsersAsync();
            foreach (var user in await users)
            {
                if (user.Services.Length.Equals(0)) { }
                else
                {
                    ProfilesWithServices.Add(new User
                    {
                        Facebookid = user.Facebookid,
                        Fname = user.Fname,
                        Lname = user.Lname,
                        Description = user.Description,
                        Imgurl = user.Imgurl,
                        Range = user.Range,
                        Zipcode = user.Zipcode,
                        Services = user.Services
                    });
                }
            }
        }

        public async void GetProfiles()
        {
            var favorService = new FavorService();
            List<User> users = await favorService.GetUsersAsync();
            foreach (var user in users)
            {
                Profiles.Add(user);
            }
        }

        public void CreateProfile(User user)
        {
            var favorService = new FavorService();
            if (CheckProfile(user) == true)
            {
                favorService.CreateProfileAsync(user);
            }
            favorService.UpdateProfileAsync(user);
        }

        public bool CheckProfile(User user)
        {
            var favorService = new FavorService();

            if (favorService.GetUserAsync(user.Facebookid) == null)
            {
                return false;
            }
            return true;
        }
    }

}

