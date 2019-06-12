using CmsShoppingCart.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CmsShoppingCart.Models.ViewModels.Shop
{
    public class ShopCategoriesVM
    {
        public ShopCategoriesVM()
        {

        }
        public ShopCategoriesVM(ShopCategoriesDTO shopCategoriesDTO)
        {
            Id = shopCategoriesDTO.Id;
            Name = shopCategoriesDTO.Name;
            Slug = shopCategoriesDTO.Slug;
            Sorting = shopCategoriesDTO.Sorting;

        }
        public int Id { get; set; }
        public string Name { get; set; }

        public string Slug { get; set; }
        public int Sorting { get; set; }

    }
}
