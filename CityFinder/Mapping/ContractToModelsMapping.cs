using CityFinder.Api;
using CityFinder.Exceptions;
using CityFinder.Models;
using System.Collections.Generic;
using System.Text.Json;

namespace CityFinder.Mapping
{
    public static class ContractToModelsMapping
    {
        public static CityResponse ToCityResponse(this CitySearchResponse citySearchResponse)
        {
            string city = null;
            JsonDocument document = JsonDocument.Parse(citySearchResponse.SearchText);
            JsonElement root = document.RootElement;
            try
            {
                JsonElement suggestionElement = root.GetProperty("suggestion");
                JsonElement locateElement = suggestionElement.GetProperty("locate");

                city = locateElement.GetString();
            }
            catch (KeyNotFoundException e)
            {
                JsonElement errorElement = root.GetProperty("error");
                JsonElement descriptionElement = errorElement.GetProperty("description");

                throw new CityNotFoundException(descriptionElement.GetString());
            }
            return new()
            {
                City = city
            };
        }
    }
}
