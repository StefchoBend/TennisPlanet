﻿using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace TennisPlanet.Models.ProductItem
{
    public class ProductItemIndexVM
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Product Name")]
        public string ItemName { get; set; }

        public int BrandId { get; set; }
        [Display(Name = "Brand")]
        public string BrandName { get; set; }

        public int CategoryId { get; set; }

        [Display(Name = "Category")]
        public string CategoryName { get; set; }

        [Display(Name = "Picture")]
        public string Picture { get; set; }

        [Display(Name ="Description")]
        public string Description { get; set; }

        [Display(Name = "Price")]
        public decimal Price { get; set; }

        [Display(Name = "Discount")]
        public decimal Discount { get; set; }
    }
}
