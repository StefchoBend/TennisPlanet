using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TennisPlanet.Infrastructure.Data.Domain
{
    public class Dimension
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Size { get; set; } = null!;


        public virtual IEnumerable<Product> Products { get; set; } = new List<Product>();
    }
}
