using FavourApp.Models;
using FavourApp.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FavourApp.ViewModels
{
    public  class ServicesViewModel
    {

        public ObservableCollection<Service> Services { get; private set; } = new ObservableCollection<Service>();

        public async void GetCategories()
        {
            var favorService = new FavorService();
            List<Service> services = await favorService.GetServicesAsync();
            foreach (var service in services)
            {
                Services.Add(service);
            }
        }
    }
}
