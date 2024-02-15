using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace TennisPlanet.Infrastructure.Data.Domain
{
    public class ProductItem
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string ItemName { get; set; } = null!;

        [Required]
        [ForeignKey("Brand")]
        public int BrandId { get; set; }
        public virtual Brand Brand { get; set; } = null!;

        [Required]
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; } = null!;

        public string Picture { get; set; } = null!;
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public virtual IEnumerable<Product> Products { get; set; } = new List<Product>();
    }
}
