using System;

namespace CityFinder.Exceptions
{
    public class CityNotFoundException : Exception
    {
        public CityNotFoundException()
        {
        }

        public CityNotFoundException(string message) : base(message)
        {
        }

        public CityNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
