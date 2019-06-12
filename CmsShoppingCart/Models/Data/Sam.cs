using System;
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
        public DbSet<ShopCategoriesDTO> ShopCategoriesDTOs { get; set; }

        public DbSet<ProductDTO> ProductDTOs { get; set; }




    }
} 