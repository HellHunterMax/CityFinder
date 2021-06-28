using System;

namespace CityFinder.Exceptions
{
    public class CountryUnknownException : Exception
    {
        public CountryUnknownException()
        {
        }

        public CountryUnknownException(string message) : base(message)
        {
        }

        public CountryUnknownException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
