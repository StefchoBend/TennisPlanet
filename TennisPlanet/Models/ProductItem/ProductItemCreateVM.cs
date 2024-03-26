using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TennisPlanet.Models.Brand;
using TennisPlanet.Models.Category;

namespace TennisPlanet.Models.ProductItem
{
    public class ProductItemCreateVM
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        [Display(Name = "Product Name")]
        public string ItemName { get; set; } = null!;

        [Required]
        [Display(Name = "Brand")]
        public int BrandId { get; set; }

        public virtual List<BrandPairVM> Brands { get; set; } = new List<BrandPairVM>();

        [Required]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        public virtual List<CategoryPairVM> Categories { get; set; } = new List<CategoryPairVM>();

        [Display(Name = "Picture")]
        public string Picture { get; set; } = null!;
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Price")]
        public decimal Price { get; set; }

        [Display(Name = "Discount")]
        public decimal Discount { get; set; }
    }
}
