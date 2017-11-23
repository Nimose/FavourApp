﻿using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using FavourApp.Models;
using FavourApp.Services;

namespace FavourApp.ViewModels
{
    public class FacebookViewModel : INotifyPropertyChanged
    {
        private FacebookProfile _facebookProfile;

        public FacebookProfile FacebookProfile
        {
            get { return _facebookProfile; }
            set
            {
                _facebookProfile = value;
                OnPropertyChanged();
            }
        }

        public async Task SetFacebookUserProfileAsync(string accessToken)
        {
            var facebookServices = new FacebookServices();

            FacebookProfile = await facebookServices.GetFacebookProfileAsync(accessToken);
            
        }

        public void RemoveFacebookUserProfile() {
            var facebookServices = new FacebookServices();
            FacebookProfile =  facebookServices.RemoveFacebookProfile();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
