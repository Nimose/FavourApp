﻿using FavourApp.Models;
using FavourApp.Services;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FavourApp
{
    public partial class UserProfile : ContentPage
	{
        User User;

        public string fbid = "";
        public UserProfile (string facebookId)
		{
            this.fbid = facebookId;
			InitializeComponent ();
        }

        protected override async void OnAppearing()
        {
            var favorService = new FavorService();
            var user = await favorService.GetUserAsync(fbid);
            this.User = user;
            UserFname.Text = user.Fname;
            UserLname.Text = user.Lname;
            UserImage.Source = user.Imgurl;
            ServiceList.ItemsSource = user.Services;
            UserDescription.Text = user.Description;
        }

        async void Message_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Message(User));
        }
    }
}