using CmsShoppingCart.Models.Data;
using CmsShoppingCart.Models.ViewModels.Pages;
using CmsShoppingCart.Models.ViewModels.Shop;
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
                shopVMs = sam.Shops.ToArray().OrderBy(x => x.Sorting).Select(x => new ShopVM(x)).ToList();


            }
            // return a view with that list 
            return View(shopVMs);
        }
        // GET: Admin/Shop/addNewCategory
        [HttpPost]
        public string addNewCategory(string newInput)
        {

            //Declare Id

            string Id;
            using (Sam sam = new Sam())
            {
                //Check that category name is unique

              if ( sam.Shops.Any(x => x.Name == newInput))
                {
                    return "titletaken";

                }
                //int the DTO 

                ShopDTO shopDTO = new ShopDTO();
                //add to DTO 
                shopDTO.Name = newInput;
                shopDTO.Slug = newInput.Replace(" ", "-").ToLower();
                shopDTO.Sorting = 100;
                //save the DTO
                sam.Shops.Add(shopDTO);
                sam.SaveChanges();
                //get the Id
                Id = shopDTO.Id.ToString();
            }

            //return Id
            return Id;
        }
    }
}