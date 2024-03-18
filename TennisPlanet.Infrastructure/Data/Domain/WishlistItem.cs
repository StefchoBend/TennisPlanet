using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TennisPlanet.Infrastructure.Data.Domain
{
    public class WishlistItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }
        public decimal Price { get; set; }
        public string Size { get; set; }
    }
}
