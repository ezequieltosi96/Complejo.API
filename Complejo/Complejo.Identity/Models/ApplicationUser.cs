﻿using Complejo.Application.Interfaces.Identity;
using Microsoft.AspNetCore.Identity;

namespace Complejo.Identity.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
