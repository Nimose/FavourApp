﻿using FavourApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FavourApp.Services
{
    public interface IRestService  
    {
        Task<List<User.Rootobject>> GetUsers();
    }
}
