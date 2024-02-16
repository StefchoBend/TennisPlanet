using System.ComponentModel.DataAnnotations;
using TennisPlanet.Models.Dimension;
using TennisPlanet.Models.ProductItem;

namespace TennisPlanet.Models.Product
{
    public class ProductCreateVM
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Product item")]
        public int ProductItemId { get; set; }

        public virtual List<ProductItemPairVM> ProductItems { get; set; } = new List<ProductItemPairVM>();
        [Required]
        [Display(Name = "Size")]
        public int DimensionId { get; set; }
        public virtual List<DimensionPairVM> Dimensions { get; set; } = new List<DimensionPairVM>();

        [Range(0, 100)]
        public int Quantity { get; set; }
    }
}
