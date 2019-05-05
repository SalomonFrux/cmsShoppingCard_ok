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
            }
            //return the view with list
            return View(pageList);


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
            if(! ModelState.IsValid)
            {
                return View(model);
            }
            using (Sam sam = new Sam()) 

            {
                // Declare Slug 
                string Slug;
                // Initial PageDTO
                PageDTO dto = new PageDTO();
                // DTO Title
                dto.Title = model.Title;
                // Check for and set Slug if need be
                if(string.IsNullOrWhiteSpace(model.Slug))
                {
                    Slug = model.Title.Replace(" ", "-").ToLower();
                }
                else
                {
                  Slug = model.Slug.Replace(" ", "-").ToLower();
                }
                // Make sure Tile and Slug are unique
                if (sam.Pages.Any(x => x.Title == model.Title) || sam.Pages.Any(x=> x.Slug == model.Slug) )
                {
                    ModelState.AddModelError(" ", "That title or slug already exist.");
                    return View(model);
                }
                // DTO the rest 
                dto.Slug = Slug;
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

    }
}  