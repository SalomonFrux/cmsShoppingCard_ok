using CmsShoppingCart.Models.ViewModels.Pages;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CmsShoppingCart.Models.Data;
using CmsShoppingCart.Models;

namespace CmsShoppingCart.Areas.Admin.Controllers
{
    public class PagesController : Controller
    {
        // GET: Admin/Pages
        public ActionResult Index()
        {
            //Declare a list of pageVM

            List<PageVM> pageList;

            using (Sam db = new Sam  () )
            {
                //Initialise that list                  
                                 pageList = db.Pages.ToArray().OrderBy(x => x.Sorting).Select(x => new PageVM(x)).ToList();
                return View(pageList);
            }
         

        } 
        // GET: Admin/Pages/AddPage
        [HttpGet]
        public ActionResult AddPage()
        {
            return View();

        }
        // POST: Admin/Pages/AddPage
        [HttpPost]
        public ActionResult AddPage(PageVM model)
        {
            // Check model state 
            if(!ModelState.IsValid)
            {
                return View(model);
            }
            using (Sam sam = new Sam()) 

            {
                // Declare Slug 
                string slug;
                // Initial PageDTO
                PageDTO dto = new PageDTO();
                // DTO Title
                dto.Title = model.Title;
                // Check for and set Slug if need be
                if(string.IsNullOrWhiteSpace(model.Slug))
                {
                    slug = model.Title.Replace(" ", "-").ToLower();
                }
                else
                {
                  slug = model.Slug.Replace(" ", "-").ToLower();
                }
                // Make sure Tile and Slug are unique
                if (sam.Pages.Any(x => x.Title == model.Title) || sam.Pages.Any(x => x.Slug == slug)) 
                {
                    ModelState.AddModelError("", "That title or slug already exist.");
                    return View(model);
                }
                // DTO the rest 
                dto.Slug = slug;
                dto.Body = model.Body;
                dto.HasSidebar = model.HasSidebar;
                dto.Sorting = 100;

                // Save DTO
                sam.Pages.Add(dto);
                sam.SaveChanges();
            }
            // Set tempData message
            TempData["SM"] = "you have successfully added a new page";

            // Redirect 
            return RedirectToAction("AddPage");

            //return View();

        }



        // POST: Admin/Pages/EditPage/Id
        [HttpGet]
        public ActionResult EditPage(int Id)
        {
            //Declare the PageVM
            PageVM model;

            //Get the page
            using (Sam sam = new Sam())
            {
                PageDTO pageDTO = sam.Pages.Find(Id);

           

            //Confirm the page exist 
          
               if(pageDTO == null)
                {
                    return Content("The page does not exist");
                }

                // initialise the PageVM

                model = new PageVM(pageDTO);
            }


            //Return the view with the model
            return View(model);
        }

        // POST: Admin/Pages/EditPage/id - this method receive the page view model

        [HttpPost]
        public ActionResult EditPage(PageVM pageVM) 
        {
            // Check the model state 
            if (!ModelState.IsValid)
            {
                return View(pageVM);
            }
            using (Sam sam = new Sam())
            {

                //Get paage Id 
                int Id = pageVM.Id;
                // declare Slug / initialise  
                string slug ="Services";
                //Get the page 
                PageDTO pageDTO = sam.Pages.Find(Id);

                // DTO the Title 
                pageDTO.Title = pageVM.Title;
                // check for Slug  Set it if need be 
                if (pageVM.Slug != "home")
                {
                    if(string.IsNullOrWhiteSpace(pageVM.Slug))
                    {
                        slug = pageVM.Title.Replace(" ", " ").ToLower();

                    }
                    else
                    {
                        slug = pageVM.Slug.Replace(" ", " ").ToLower();
                    }
                }


                // check if Title and slug are unique
                if (sam.Pages.Where(element => element.Id != pageVM.Id).Any(element => element.Title == pageVM.Title) || 
                    sam.Pages.Where(element => element.Id != pageVM.Id).Any(element => element.Slug == slug))
                {
                   ModelState.AddModelError("", "That Title od Slug already Exist");
                    return View(pageVM);
                }

                //DTO the rest 
                pageDTO.HasSidebar = pageVM.HasSidebar;
                pageDTO.Slug = slug;
                pageDTO.Body = pageVM.Body;

                // save the DTO 
                sam.SaveChanges();
               
            }
            // Set the TemData message 

            TempData["SM"] = "You have successfully edited the page!" ;
            // redirect 

            return RedirectToAction("EditPage");
        }

        // GET: Admin/Pages/PageDetails/Id

        public ActionResult PageDetails(int Id) // Give the page details based on the Id
        {
            // declare the pageVm 
            PageVM pageVM;
            // Get the page
            using (Sam sam = new Sam())
            {
                PageDTO pageDTO = sam.Pages.Find(Id);

            //Confirm the page exist 
            if(pageDTO == null)
                {
                    return Content("The page does not exist");

                }
                //Init the PageVm
                pageVM = new PageVM(pageDTO);
            }
            //Return the view modal 
            return View(pageVM);
            }
        }
}  