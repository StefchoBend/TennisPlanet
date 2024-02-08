using System.ComponentModel.DataAnnotations;
using TennisPlanet.Models.Brand;
using TennisPlanet.Models.Category;

namespace TennisPlanet.Models.Product
{
    public class ProductDeleteVM
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Product Name")]
        public string ProductName { get; set; }

        [Display(Name = "Size")]
        public string Size { get; set; }

        [Display(Name = "QuantityInStock")]
        public int QuantityInStock { get; set; }
    }
}
