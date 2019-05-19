using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CmsShoppingCart.Models.Data
{
    [Table("tblCategories")]
    public class ShopDTO
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public string Slug { get; set; }
        public int Sorting { get; set; }
    }
}