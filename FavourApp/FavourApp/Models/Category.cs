namespace FavourApp.Models
{
    public class Category
    {
        public class Rootobject
        {
            public Categories[] categories { get; set; }
        }

        public class Categories
        {
            public string _id { get; set; }
            public string name { get; set; }
        }

    }
}
