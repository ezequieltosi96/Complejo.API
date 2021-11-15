using System;
using System.ComponentModel.DataAnnotations;

namespace Complejo.Application.Models.Identity.Registration
{
    public class RegistrationRequest
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(8)]
        public string Password { get; set; }

        [Required]
        public string RoleName { get; set; }

        [Required]
        [MaxLength(8)]
        [MinLength(8)]
        public string Dni { get; set; }

        [Required]
        [MaxLength(13)]
        public string PhoneNumber { get; set; }

        public Guid? IdClient { get; set; }
    }
}
