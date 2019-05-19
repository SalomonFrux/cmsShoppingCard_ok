﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CmsShoppingCart.Models.Data
{
    public class Sam :DbContext 
    {
        public DbSet<PageDTO> Pages  { get; set; }
        public DbSet<SidebarDTO> Sidebar { get; set; }

    }
} 