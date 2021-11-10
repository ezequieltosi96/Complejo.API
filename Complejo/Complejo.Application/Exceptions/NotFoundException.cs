using Complejo.Application.Exceptions.Base;
using System;

namespace Complejo.Application.Exceptions
{
    public class NotFoundException : BaseException
    {
        public NotFoundException(string name, object key) : base($"{name} ({key}) not found.")
        {
        }

        public NotFoundException(string name, Guid? key) : base(key.HasValue ? $"{name} ({key}) not found." : $"Invalid Id.")
        {
        }
    }
}
