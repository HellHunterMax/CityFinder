using CityFinder.Exceptions;
using CityFinder.Models;
using CityFinder.Services;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace CityFinder.Api
{
    public class CityController : ICityFinderApi
    {
        private static readonly HttpClient _HttpClient;
        private readonly ICountryAndPostCodeUriBuilder _uriBuilder;

        static CityController()
        {
            _HttpClient = new HttpClient();
        }

        public CityController(ICountryAndPostCodeUriBuilder uriBuilder)
        {
            _uriBuilder = uriBuilder;
        }

        public async Task<CitySearchResponse> SearchByPostcodeAsync(string searchString)
        {
            try
            {
                Console.WriteLine($"MAKING CALL TO: {searchString}");
                CitySearchResponse response = new();

                response.SearchText = await _HttpClient.GetStringAsync(searchString);
                return response;

            }
            catch (HttpRequestException e)
            {
                throw new FailedToCallException($"Message :{e.Message} ");
            }
        }

        public async Task<string> GetCityAsync(CountryAndPostcode countryAndPostCode)
        {
            string uri = _uriBuilder.BuildUriFromCountryAndPostCode(countryAndPostCode);
            string responseBody = String.Empty;
            string city = null;

            try
            {
                Console.WriteLine($"MAKING CALL TO: {uri}");

                responseBody = await _HttpClient.GetStringAsync(uri);

            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException in the call caught!");
                Console.WriteLine($"Message :{e.Message} ");
                Console.ReadKey();
            }
            try
            {
                JsonDocument document = JsonDocument.Parse(responseBody);
                JsonElement root = document.RootElement;
                JsonElement suggestionElement = root.GetProperty("suggestion");
                JsonElement locateElement = suggestionElement.GetProperty("locate");

                city = locateElement.GetString();
            }
            catch (KeyNotFoundException e)
            {
                JsonDocument document = JsonDocument.Parse(responseBody);
                JsonElement root = document.RootElement;
                JsonElement errorElement = root.GetProperty("error");
                JsonElement descriptionElement = errorElement.GetProperty("description");

                Console.WriteLine($"{descriptionElement.GetString()}");
                Console.ReadKey();
            }


            return city;
        }


    }
}
