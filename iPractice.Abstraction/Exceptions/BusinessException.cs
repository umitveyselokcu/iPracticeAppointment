using System;

namespace iPractice.Abstraction.Exceptions
{
    public class BusinessException : ExceptionBase
    {
        public BusinessException(string message)
            : base(message)
        {
        }

        public BusinessException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}