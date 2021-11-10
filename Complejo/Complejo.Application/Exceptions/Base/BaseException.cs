using System;

namespace Complejo.Application.Exceptions.Base
{
    [Serializable]
    public abstract class BaseException : ApplicationException
    {
        protected BaseException()
        {
        }

        protected BaseException(String message) : base(message)
        {
        }

        protected BaseException(String message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
