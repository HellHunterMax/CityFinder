using System;

namespace CityFinder.Exceptions
{
    public class FailedToCallException : Exception
    {
        public FailedToCallException()
        {
        }

        public FailedToCallException(string message) : base(message)
        {
        }

        public FailedToCallException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
