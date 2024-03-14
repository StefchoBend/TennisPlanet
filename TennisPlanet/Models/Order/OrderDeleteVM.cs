using System.ComponentModel.DataAnnotations;

namespace TennisPlanet.Models.Order
{
    public class OrderDeleteVM
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ItemName { get; set; } = null!;
        public string Picture { get; set; } = null!;
        public string Size { get; set; }
        public int CountOfProducts { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
