namespace FavourApp.Models
{
    public class User
    {     
            public string Facebookid { get; set; }
            public string Description { get; set; }
            public int Range { get; set; }
            public string Zipcode { get; set; }
            public string Fname { get; set; }
            public string Lname { get; set; }
            public string Imgurl { get; set; }
            public Service[] Services { get; set; }

    }
}
