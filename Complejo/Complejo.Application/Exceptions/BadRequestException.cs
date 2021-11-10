using Complejo.Application.Exceptions.Base;
using System;

namespace Complejo.Application.Exceptions
{
    public class BadRequestException : BaseException
    {
        public BadRequestException(string message) : base(message)
        {
        }

        public BadRequestException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
