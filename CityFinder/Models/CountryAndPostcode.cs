using CityFinder.Exceptions;
using System.Globalization;
using System.Linq;

namespace CityFinder.Models
{
    public class CountryAndPostcode
    {
        public RegionInfo Region { get; set; }
        public string Postcode { get; set; }

        public static CountryAndPostcode Parse(string countryCode, string postcode)
        {
            var isCountryCodeValid = IsCountryCodeValid(countryCode);

            if (isCountryCodeValid)
            {
                return new CountryAndPostcode()
                {
                    Region = new RegionInfo(countryCode),
                    Postcode = postcode
                };
            }

            throw new CountryUnknownException($"Countrycode {countryCode} is unknown. Please check code again and retry.");
        }
        private static bool IsCountryCodeValid(string countryCode)
        {
            return CultureInfo
                .GetCultures(CultureTypes.SpecificCultures)
                    .Select(culture => new RegionInfo(culture.LCID))
                        .Any(region => region.TwoLetterISORegionName == countryCode);
        }
    }
}
