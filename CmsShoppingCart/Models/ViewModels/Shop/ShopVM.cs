﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CmsShoppingCart.Models.Data;
namespace CmsShoppingCart.Models.ViewModels.Pages
{
    public class ShopVM
    {

        public ShopVM()
        {

        }
        public ShopVM(ShopDTO row)
        {
            Id = row.Id;
            Name = row.Name;
            Slug = row.Slug;
            Sorting = row.Sorting;

        }
        public int Id { get; set; }
        public string Name { get; set; }

        public string Slug { get; set; }
        public int Sorting { get; set; }

    }
}