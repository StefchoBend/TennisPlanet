using System.ComponentModel.DataAnnotations;


namespace TennisPlanet.Infrastructure.Data.Domain
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string CategoryName { get; set; } = null!;

        public virtual IEnumerable<ProductItem> ProductItems { get; set; } = new List<ProductItem>();
    }
}
