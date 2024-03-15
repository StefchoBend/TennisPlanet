using System.ComponentModel.DataAnnotations;
using TennisPlanet.Infrastructure.Data.Domain;

namespace TennisPlanet.Models.Wishlist
{
    public class WishlistIndexVM
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Product Name")]
        public string Name { get; set; }
        [Display(Name = "Picture")]
        public string Picture { get; set; }
        [Display(Name = "Price")]
        public decimal Price { get; set; }
        [Display(Name = "Size")]
        public string Size { get; set; }    
        public List<WishlistItem> WishlistItems { get; set; }
    }
}
