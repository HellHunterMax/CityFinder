using CityFinder.Api;
using CityFinder.Output;
using CityFinder.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace CityFinder
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var configuration = BuildConfiguration();
            var serviceProvider = BuildServiceProvider(configuration);
            var app = serviceProvider.GetRequiredService<CityFinderApp>();

            await app.RunAsync(args);

        }
        private static void ConfigureServices(IServiceCollection services, IConfigurationRoot configuration)
        {
            services.AddSingleton<CityFinderApp>();
            services.AddSingleton<IConsoleWriter, ConsoleWriter>();
            var baseAddress = configuration["CityFinderApi:BaseAddress"];
            services.AddSingleton<ICountryAndPostCodeUriBuilder, CountryAndPostCodeUriBuilder>(x => new CountryAndPostCodeUriBuilder(baseAddress));
            services.AddSingleton<ICitySearchService, CitySearchService>();
            services.AddSingleton<ICityFinderApi, CityController>();
        }

        private static ServiceProvider BuildServiceProvider(IConfigurationRoot configuration)
        {
            ServiceCollection services = new ServiceCollection();
            ConfigureServices(services, configuration);
            var serviceProvider = services.BuildServiceProvider();
            return serviceProvider;
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            var configuration = new ConfigurationBuilder()
                            .AddJsonFile("appsettings.json")
                            .Build();
            return configuration;
        }
    }
}
