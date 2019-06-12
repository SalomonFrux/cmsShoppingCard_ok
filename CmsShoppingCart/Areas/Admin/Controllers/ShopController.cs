using CmsShoppingCart.Models.Data;
using CmsShoppingCart.Models.ViewModels.Shop;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace CmsShoppingCart.Areas.Admin.Controllers
{
    public class ShopController : Controller
    {
        // GET: Admin/ShopCategories
        public ActionResult ShopCategories()
        {
            //Let's declare a list of categories 
            List<ShopCategoriesVM> ListShopCategoriesVM;
            using(Sam sam = new Sam())
            {
            //int the DTO 
            ShopCategoriesDTO shopCategoriesDTO = new ShopCategoriesDTO();

                //Int the list of VM
                ListShopCategoriesVM = sam.ShopCategoriesDTOs.ToArray().OrderBy(x => x.Sorting).Select(x => new ShopCategoriesVM(x)).ToList();

            }
            // return the list in teh View 
            return View(ListShopCategoriesVM);
        }

        //Post: Admin/Shop/AddNewCatedory/
        public string AddNewCatedory(string NewCategoryInputValue)
        {
            //Declare the Id to return
            string id;
            //int the VM
            ShopCategoriesDTO shopCategoriesDTO = new ShopCategoriesDTO();
            using (Sam sam = new Sam())
            {
                //make sure the category is unique 
                if (sam.ShopCategoriesDTOs.Any(x => x.Name == NewCategoryInputValue))
                    return "titletaken";
                //DTO the rest 
                shopCategoriesDTO.Name = NewCategoryInputValue;
                shopCategoriesDTO.Slug = NewCategoryInputValue.Replace(" ", "-").ToLower();
                shopCategoriesDTO.Sorting = 100;

                //Save the changes
               sam.ShopCategoriesDTOs.Add(shopCategoriesDTO);
                sam.SaveChanges();
                 id = shopCategoriesDTO.Id.ToString();
            }
            return id;
        }

        //Post: Admin/Shop/ReoraganizeCategory/Id
        [HttpPost]
        public void ReoraganizeCategory(int[] Id)
        {
           
            using (Sam sam = new Sam())
            {
                //int DTO
                ShopCategoriesDTO shopCategoriesDTO = new ShopCategoriesDTO();
                //int a count 
                int count = 1;
                foreach (var Category in Id)
                {
                    shopCategoriesDTO = sam.ShopCategoriesDTOs.Find(Category);
                    shopCategoriesDTO.Sorting = count;
                    sam.SaveChanges();
                    count++;

                }

            }

        }


        //Get: Admin/Shop/ReoraganizeCategory/Id
        [HttpGet]
        public ActionResult DeleteCategory(int Id)
        {
            using(Sam sam = new Sam())
            {
                //Declare the DTO
                ShopCategoriesDTO shopCategoriesDTO;
                //find the Id to delete
                shopCategoriesDTO = sam.ShopCategoriesDTOs.Find(Id);
                //Remove the page with Id passed in 
                sam.ShopCategoriesDTOs.Remove(shopCategoriesDTO);
                //save changes 
                sam.SaveChanges();

            }

            return RedirectToAction("ShopCategories");
        }

        public string UpdateCategory(string CategoryInputValue)
        {
            using(Sam sam = new Sam())
            {
            //int DTO
            ShopCategoriesDTO shopCategoriesDTO = new ShopCategoriesDTO();

                //check if what CategoryInputValue exits in DTO
                if (sam.ShopCategoriesDTOs.Any(x => x.Name == CategoryInputValue))
                    return "titletaken";
                //else DTO the rest
                shopCategoriesDTO.Name = CategoryInputValue;
                shopCategoriesDTO.Slug = CategoryInputValue.Replace(" ", "-").ToLower();
                sam.SaveChanges();

            }
            return "sam";
        }

        
        //Get: Admin/Shop/AddProduct
        [HttpGet]   
        public ActionResult AddProduct()
        {
            ProductsVM productsVM = new ProductsVM();

            using (Sam sam = new Sam())
            {

                productsVM.Category = new SelectList(sam.ShopCategoriesDTOs.ToList(), "Id", "Name");
                
            }

            return View(productsVM);
        }
        //Post: Admin/Shop/AddProduct
        [HttpPost]
        public ActionResult AddProduct( ProductsVM productsVM, HttpPostedFileBase file)
        {

            int ProductPostedID;
            if (!ModelState.IsValid)
            {
                using(Sam sam = new Sam())
                {
                    productsVM.Category = new SelectList(sam.ShopCategoriesDTOs.ToList(), "Id", "Name");
                    return View(productsVM);
                }
            }


            using (Sam sam = new Sam())
            {
                //check if the name being added does not exist in the database table
                
                if (sam.ProductDTOs.Any(x => x.Name == productsVM.Name))
                {
                    productsVM.Category = new SelectList(sam.ShopCategoriesDTOs.ToList(), "Id", "Name");
                    ModelState.AddModelError("", "The Product name you are trying to add already exist");

                    return View(productsVM);

                }
                //DTO The rest
                ProductDTO productDTO = new ProductDTO();

                productDTO.Name = productsVM.Name;
                productDTO.Slug = productsVM.Name.Replace(" ", "-").ToLower();
                productDTO.Description = productsVM.Description;
                productDTO.Price = productsVM.Price;
                productDTO.CategoryId = productsVM.CategoryId;
                // productsVM.ImageName = productsVM.ImageName;

                  ShopCategoriesDTO  shopCategoriesDTO = sam.ShopCategoriesDTOs.FirstOrDefault(x => x.Id == productsVM.CategoryId);
                productDTO.CategoryName = shopCategoriesDTO.Name;

                sam.ProductDTOs.Add(productDTO);
                sam.SaveChanges();

                ProductPostedID = productDTO.Id;
            }
            TempData["sam-notification"] = "You have have added a product";
           
            
            #region Uploading an image
            //Declare the image directories

            var OriginaiImgDirectory = new DirectoryInfo(string.Format("{0}Images\\Uploads", Server.MapPath(@"\")));

            var PathString1 = Path.Combine(OriginaiImgDirectory.ToString(), "Products");
            var PathString2 = Path.Combine(OriginaiImgDirectory.ToString(), "Products" + ProductPostedID.ToString());
            var PathString3 = Path.Combine(OriginaiImgDirectory.ToString(), "Products" + ProductPostedID.ToString() + "\\Thumbs");
            var PathString4 = Path.Combine(OriginaiImgDirectory.ToString(), "Products" + ProductPostedID.ToString() + "\\Galery");
            var PathString5 = Path.Combine(OriginaiImgDirectory.ToString(), "Products" + ProductPostedID.ToString() + "\\Galery\\Thumbs");
            
            //Create the images directories if the do not exits 

            if (!Directory.Exists(PathString1))
                Directory.CreateDirectory(PathString1);

            if (!Directory.Exists(PathString2))
                Directory.CreateDirectory(PathString2);

            if (!Directory.Exists(PathString3))
                Directory.CreateDirectory(PathString3);

            if (!Directory.Exists(PathString4))
                Directory.CreateDirectory(PathString4);

            if (!Directory.Exists(PathString5))
                Directory.CreateDirectory(PathString5);

            // check if a file was uploaded 
            if (file != null && file.ContentLength > 0)
            {
                //Get the file extension and check to see if it is an image
                string ext = file.ContentType.ToLower();

                if (ext != "image/jpeg" && ext != "image/gif" && ext != "image/jpg" && ext != "image/jpeg" && ext != "image/png" && ext != "image/x-png")
                {
                    using (Sam sam = new Sam())
                    {
                        productsVM.Category = new SelectList(sam.ShopCategoriesDTOs.ToList(), "Id", "Name");
                        ModelState.AddModelError("", "The images was not saved - wrong file extension");
                        return View(productsVM);

                    }

                }

                //int imageName column that we have in the table to the user image name
                string ImageName = file.FileName;
                //Save the image in the DTP through entity
                using (Sam sam = new Sam())
                {
                    ProductDTO productDTO;// new ProductDTO();
                    productDTO = sam.ProductDTOs.Find(ProductPostedID);
                    productDTO.ImageName = ImageName;
                    sam.SaveChanges();

                }
                //set the Original image path and its thumb
                var path = string.Format("{0}\\{1}", PathString2, ImageName);
                var path2 = string.Format("{0}\\{1}", PathString3, ImageName);

                //Save the original image
                file.SaveAs(path);

                //Create the thumb and save it
                WebImage image = new WebImage(file.InputStream);
                image.Resize(200, 200);
                image.Save(path2);
            }



            #endregion

            return RedirectToAction("AddProduct");
        }

    }
    
}