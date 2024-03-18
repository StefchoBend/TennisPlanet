using System.ComponentModel.DataAnnotations;

namespace TennisPlanet.Models.Product
{
    public class ProductDetailsVM
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Product Name")]
        public string ItemName { get; set; } = null!;

        public int BrandId { get; set; }
        [Display(Name = "Brand")]
        public string BrandName { get; set; } = null!;

        public int CategoryId { get; set; }
        [Display(Name = "Category")]
        public string CategoryName { get; set; } = null!;

        [Display(Name = "Picture")]
        public string Picture { get; set; } = null!;

        [Display(Name = "Size")]
        public string Size { get; set; }

        [Display(Name = "QuantityInStock")]
        public int QuantityInStock { get; set; }

        [Display(Name = "Price")]
        public decimal Price { get; set; }

        [Display(Name = "Discount")]
        public decimal Discount { get; set; }
        public bool IsInWishlist { get; set; }
    }
}
