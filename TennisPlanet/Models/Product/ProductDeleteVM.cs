using System.ComponentModel.DataAnnotations;
using TennisPlanet.Models.Brand;
using TennisPlanet.Models.Category;
using TennisPlanet.Models.Dimension;
using TennisPlanet.Models.ProductItem;

namespace TennisPlanet.Models.Product
{
    public class ProductDeleteVM
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

        public int DimensionId { get; set; }
        [Display(Name = "Size")]
        public string Size { get; set; }

        [Display(Name = "Picture")]
        public string Picture { get; set; } = null!;
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Range(0, 100)]
        public int Quantity { get; set; }

        [Display(Name = "Price")]
        public decimal Price { get; set; }

        [Display(Name = "Discount")]
        public decimal Discount { get; set; }

    }
}
