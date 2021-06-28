using CityFinder.Models;
using System.Text;

namespace CityFinder.Services
{
    public class CountryAndPostCodeUriBuilder : ICountryAndPostCodeUriBuilder
    {
        private readonly string _apiUri;

        public CountryAndPostCodeUriBuilder(string apiUri)
        {
            _apiUri = apiUri;
        }
        public string BuildUriFromCountryAndPostCode(CountryAndPostcode countryAndPostCode)
        {
            StringBuilder sb = new();
            sb.Append($"{_apiUri}/");
            sb.Append($"?region={countryAndPostCode.Region.TwoLetterISORegionName}");
            sb.Append("&geoit=json");
            sb.Append($"&streetname={countryAndPostCode.Postcode}");

            return sb.ToString(); //$"{_apiUri}/?region={countryAndPostCode.Region.TwoLetterISORegionName}&geoit=json&streetname={countryAndPostCode.Postcode}"
        }
    }
}
