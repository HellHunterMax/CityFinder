using CommandLine;

namespace CityFinder
{
    public class CityFinderOptions
    {
        [Option('c', "CountryCode", Required = true, HelpText = "Provide the Two Letter Country Code to search City for.")]
        public string CountryCode { get; init; }
        [Option('p', "PostCode", Required = true, HelpText = "Provide the PostCode to search City for.")]
        public string PostCode { get; set; }
    }
}
