using System;

namespace Complejo.Application.Models.Identity.User
{
    public class User
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string RoleName { get; set; }

        public Guid? IdClient { get; set; }

    }
}
