using CityFinder.Models;

namespace CityFinder.Services
{
    public interface ICountryAndPostCodeUriBuilder
    {
        string BuildUriFromCountryAndPostCode(CountryAndPostcode countryAndPostCode);
    }
}
