using CityFinder.Exceptions;
using CityFinder.Models;
using CityFinder.Output;
using CityFinder.Services;
using CommandLine;
using System.Threading.Tasks;

namespace CityFinder
{
    public class CityFinderApp
    {
        private readonly IConsoleWriter _consoleWriter;
        private readonly ICitySearchService _citySearchService;
        public CityFinderApp(IConsoleWriter consoleWriter, ICitySearchService citySearchService)
        {
            _consoleWriter = consoleWriter;
            _citySearchService = citySearchService;
        }

        public async Task RunAsync(string[] args)
        {
            await Parser.Default
                .ParseArguments<CityFinderOptions>(args)
                .WithParsedAsync(async option =>
                {
                    CityRequest cityRequest = new()
                    {
                        CountyCode = option.CountryCode,
                        Postcode = option.PostCode
                    };

                    try
                    {
                        var result = await _citySearchService.SearchByCountryAndPostCodeAsync(cityRequest);

                        _consoleWriter.WriteLine($"The City you are looking for is probably: \"{result.City}\".");
                    }
                    catch (CityNotFoundException e)
                    {
                        _consoleWriter.WriteLine(e.Message);
                    }
                    catch (CountryUnknownException e)
                    {
                        _consoleWriter.WriteLine(e.Message);
                    }
                    catch (FailedToCallException e)
                    {
                        _consoleWriter.WriteLine(e.Message);
                    }
                });
        }
    }
}
