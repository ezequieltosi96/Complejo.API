using System;
using System.ComponentModel.DataAnnotations;

namespace Complejo.Application.Queries.Base
{
    public class GetAllPagedBaseQuery
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int Page { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Size { get; set; }

    }
}
