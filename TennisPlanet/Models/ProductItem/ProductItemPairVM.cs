using System.ComponentModel.DataAnnotations;

namespace TennisPlanet.Models.ProductItem
{
    public class ProductItemPairVM
    {
        public int Id { get; set; }

        [Display(Name = "Product item")]
        public string ProductItemName { get; set; } = null!;
    }
}
