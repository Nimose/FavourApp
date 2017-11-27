using FavourApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FavourApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Message : ContentPage
	{
        User User;
		public Message (User user)
		{
            this.User = user;
			InitializeComponent ();
		}

        private void SendMessage_Clicked(object sender, EventArgs e)
        {

        }
    }
}