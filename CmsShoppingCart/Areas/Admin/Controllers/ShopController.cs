using CmsShoppingCart.Models.Data;
using CmsShoppingCart.Models.ViewModels.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CmsShoppingCart.Areas.Admin.Controllers
{
    public class ShopController : Controller
    {
        // GET: Admin/Shop/CategoriesPage
        public ActionResult CategoriesPage()
        {
            // Declarethe list of View Models
            List<ShopVM> shopVMs;
            using (Sam sam = new Sam())
            {
                //Int that list 
                shopVMs = sam.Shops
                    .ToArray()
                    .OrderBy(x => x.Sorting)
                    .Select(x => new ShopVM(x))
                    .ToList();


            }
            // return a view with that list 
            return View();
        }
    }
}