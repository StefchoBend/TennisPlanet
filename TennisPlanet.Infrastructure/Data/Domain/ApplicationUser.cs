using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Identity;

namespace TennisPlanet.Infrastructure.Data.Domain
{
    public class ApplicationUser:IdentityUser
    {
        [Required]
        [MaxLength(30)]
        public string FirstName { get; set; } = null!;

        [Required]
        [MaxLength(30)]
        public string LastName { get; set; } = null!;

        [Required]
        [MaxLength(30)]
        public string Address { get; set; } = null!;
    }
}
