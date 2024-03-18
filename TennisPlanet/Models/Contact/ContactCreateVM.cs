using System.ComponentModel.DataAnnotations;

namespace TennisPlanet.Models.Contact
{
    public class ContactCreateVM
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        [Display(Name = "Name")]
        public string FirstName { get; set; } = null!;

        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; } = null!;

        [Required]
        [Display(Name = "Message")]
        public string Message { get; set; } = null!;
    }
}
