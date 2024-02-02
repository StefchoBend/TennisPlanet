using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
    

namespace TennisPlanet.Infrastructure.Data.Domain
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [ForeignKey("ProductItem")]
        public int ProductItemId { get; set; }
        public virtual ProductItem ProductItem { get; set; } = null!;

        [Required]
        [ForeignKey("Dimension")]
        public int DimensionId { get; set; }
        public virtual Dimension Dimension { get; set; } = null!;

        [Range(0, 100)]
        public int Quantity { get; set; }

        public virtual IEnumerable<Order> Orders { get; set; } = new List<Order>();
    }
}
