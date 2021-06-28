using CityFinder.Models;
using System.Threading.Tasks;

namespace CityFinder.Services
{
    public interface ICitySearchService
    {
        Task<CityResponse> SearchByCountryAndPostCodeAsync(CityRequest cityRequest);
    }
}