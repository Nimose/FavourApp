using FavourApp.Models;
using FavourApp.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FavourApp.ViewModels
{
    public class CategoriesViewModel
    {
        public ObservableCollection<Category> Categories { get; private set; } = new ObservableCollection<Category>();

        public async void GetCategories()
        {
            var favorService = new FavorService();
            List<Category> categories = await favorService.GetCategoriesAsync();
            foreach (var category in categories)
            {
                Categories.Add(category);
            }
        }
    }
}
