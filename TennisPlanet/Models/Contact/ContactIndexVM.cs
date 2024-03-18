using System.ComponentModel.DataAnnotations;

namespace TennisPlanet.Models.Contact
{
    public class ContactIndexVM
    {
        public int Id { get; set; }

        [Display(Name = "Name")]
        public string FirstName { get; set; } = null!;

        [Display(Name = "Email")]
        public string Email { get; set; } = null!;

        [Display(Name = "Message")]
        public string Message { get; set; } = null!;
    }
}
