using System.ComponentModel.DataAnnotations;

namespace TennisPlanet.Models.Dimension
{
    public class DimensionPairVM
    {
        public int Id { get; set; }

        [Display(Name = "Size")]
        public string Size { get; set; } = null!;
    }
}
