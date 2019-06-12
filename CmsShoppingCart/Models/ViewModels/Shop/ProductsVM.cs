using CmsShoppingCart.Models.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CmsShoppingCart.Models.ViewModels.Shop
{
    public class ProductsVM
    {
        public ProductsVM()
        {

        }
        public ProductsVM(ProductDTO productDTORow)
        {
            Id = productDTORow.Id;
            Name = productDTORow.Name;
            Slug = productDTORow.Slug;
            Description = productDTORow.Description;
            Price = productDTORow.Price;
            CategoryName = productDTORow.CategoryName;
            CategoryId = productDTORow.CategoryId;
            ImageName = productDTORow.ImageName;



        }
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string CategoryName { get; set; }

        [Required]
        public int CategoryId { get; set; }
        public string ImageName { get; set; }

        public IEnumerable<SelectListItem> Category { get; set; }
        public IEnumerable<string> GaleryImages { get; set; }

    }
}