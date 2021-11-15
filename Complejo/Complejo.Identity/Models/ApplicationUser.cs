using Complejo.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;

namespace Complejo.Identity.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Guid? IdClient { get; set; }
    }
}
