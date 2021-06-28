using CityFinder.Api;
using CityFinder.Exceptions;
using CityFinder.Models;
using CityFinder.Services;
using FluentAssertions;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace CityFinderTests
{
    public class CitySearchServiceTests
    {
        private readonly CitySearchService _sut;
        private readonly Mock<ICityFinderApi> _cityFinderApi = new();
        private readonly Mock<ICountryAndPostCodeUriBuilder> _countryAndPostCodeUriBuilder = new();

        private const string city = "{ \"suggestion\":{ \"locate\":\"Amsterdam.\" }}";
        private const string error = "{\"error\":{\"description\":\"No City Found.\"}}";

        public CitySearchServiceTests()
        {
            _sut = new CitySearchService(_cityFinderApi.Object, _countryAndPostCodeUriBuilder.Object);
        }

        [Fact]
        public void SearchByCountryAndPostCodeAsync_GivenNonExistingCountry_ThrowsCountryUnknownException()
        {
            //Arrange
            var countryCode = "PP";
            var postcode = "1001AC";

            CityRequest cityRequest = new()
            {
                CountyCode = countryCode,
                Postcode = postcode
            };

            //Act
            Func<Task> act = async () => await _sut.SearchByCountryAndPostCodeAsync(cityRequest);

            //Assert
            act.Should().Throw<CountryUnknownException>()
                .WithMessage($"Countrycode {countryCode} is unknown. Please check code again and retry.");
        }

        [Fact]
        public async void SearchByCountryAndPostCodeAsync_GivenValidInput_ReturnsCity()
        {
            //Arrange
            var countryCode = "NL";
            var postcode = "1001AC";
            var Amsterdam = "Amsterdam.";
            CityResponse expected = new() { City = Amsterdam };
            var citySearchResponse = new CitySearchResponse() { SearchText = city };

            CityRequest cityRequest = new()
            {
                CountyCode = countryCode,
                Postcode = postcode
            };
            _countryAndPostCodeUriBuilder.Setup(c => c.BuildUriFromCountryAndPostCode(It.IsAny<CountryAndPostcode>())).Returns(() => "Test");
            _cityFinderApi.Setup(c => c.SearchByPostcodeAsync("Test").Result).Returns(() => citySearchResponse);

            //Act
            var actual = await _sut.SearchByCountryAndPostCodeAsync(cityRequest);

            //Assert
            actual.Should().Be(expected);
        }
        [Fact]
        public void SearchByCountryAndPostCodeAsync_GivenInValidInput_ReturnsCityNotFoundException()
        {
            //Arrange
            var countryCode = "NL";
            var postcode = "1001AC";
            var citySearchResponse = new CitySearchResponse() { SearchText = error };

            CityRequest cityRequest = new()
            {
                CountyCode = countryCode,
                Postcode = postcode
            };
            _countryAndPostCodeUriBuilder.Setup(c => c.BuildUriFromCountryAndPostCode(It.IsAny<CountryAndPostcode>())).Returns(() => "Test");
            _cityFinderApi.Setup(c => c.SearchByPostcodeAsync("Test").Result).Returns(() => citySearchResponse);

            //Act
            Func<Task> act = async () => await _sut.SearchByCountryAndPostCodeAsync(cityRequest);

            //Assert
            act.Should().Throw<CityNotFoundException>()
                .WithMessage("No City Found.");
        }
    }
}
