using System.ComponentModel.DataAnnotations;



namespace TennisPlanet.Infrastructure.Data.Domain
{
    public class Brand
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string BrandName { get; set; } = null!;

        public virtual IEnumerable<ProductItem> ProductItems { get; set; } = new List<ProductItem>();
    }
}
