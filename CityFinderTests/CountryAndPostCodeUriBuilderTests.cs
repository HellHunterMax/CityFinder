using CityFinder.Models;
using CityFinder.Services;
using FluentAssertions;
using Xunit;

namespace CityFinderTests
{
    public class CountryAndPostCodeUriBuilderTests
    {
        private readonly string baseAddress = "test";
        private readonly string country = "NL";
        private readonly string postcode = "1111AB";
        private readonly CountryAndPostCodeUriBuilder _sut;

        public CountryAndPostCodeUriBuilderTests()
        {
            _sut = new(baseAddress);
        }

        [Fact]
        public void BuildUriFromCountryAndPostCode_GivenCountryAndPostCode_ReturnsValidString()
        {
            //Arrange
            CountryAndPostcode countryAndPostcode = new()
            {
                Region = new System.Globalization.RegionInfo(country),
                Postcode = postcode
            };
            string expected = $"{baseAddress}/?region={country}&geoit=json&streetname={postcode}";

            //Act
            var actual = _sut.BuildUriFromCountryAndPostCode(countryAndPostcode);

            //Assert
            actual.Should().Be(expected);
        }
    }
}
