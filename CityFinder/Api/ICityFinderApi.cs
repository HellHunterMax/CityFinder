using System.Threading.Tasks;

namespace CityFinder.Api
{
    public interface ICityFinderApi
    {
        Task<CitySearchResponse> SearchByPostcodeAsync(string searchString);
    }
}
