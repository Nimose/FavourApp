﻿using System;
using System.Collections.Generic;

namespace FavourApp.Models
{
    public class User
    {
     
        public class Rootobject
        {
            public Users[] users { get; set; }
        }

        public class Users
        {
            public string facebookid { get; set; }
            public string description { get; set; }
            public int range { get; set; }
            public string zipcode { get; set; }
            public string fname { get; set; }
            public string lname { get; set; }
            public string imgurl { get; set; }
            public Service[] services { get; set; }
        }

        public class Service
        {
            public string category { get; set; }
            public int price { get; set; }
        }

    }
}