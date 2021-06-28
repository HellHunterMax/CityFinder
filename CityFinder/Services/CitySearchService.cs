using CityFinder.Api;
using CityFinder.Mapping;
using CityFinder.Models;
using System;
using System.Threading.Tasks;

namespace CityFinder.Services
{
    public class CitySearchService : ICitySearchService
    {
        public readonly ICityFinderApi _cityFinderApi;
        public readonly ICountryAndPostCodeUriBuilder _uriBuilder;
        public CitySearchService(ICityFinderApi cityFinderApi, ICountryAndPostCodeUriBuilder uriBuilder)
        {
            _cityFinderApi = cityFinderApi;
            _uriBuilder = uriBuilder;
        }

        public async Task<CityResponse> SearchByCountryAndPostCodeAsync(CityRequest cityRequest)
        {
            CountryAndPostcode countryAndPostcode = CountryAndPostcode.Parse(cityRequest.CountyCode, cityRequest.Postcode);

            var searchString = _uriBuilder.BuildUriFromCountryAndPostCode(countryAndPostcode);
            CitySearchResponse response = await _cityFinderApi.SearchByPostcodeAsync(searchString);

            CityResponse cityResponse = response.ToCityResponse();
            return cityResponse;
        }
    }
}
