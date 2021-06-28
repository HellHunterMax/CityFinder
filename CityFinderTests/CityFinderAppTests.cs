using CityFinder;
using CityFinder.Exceptions;
using CityFinder.Models;
using CityFinder.Output;
using CityFinder.Services;
using Moq;
using Xunit;

namespace CityFinderTests
{
    public class CityFinderAppTests
    {
        private readonly CityFinderApp _sut;
        private readonly Mock<IConsoleWriter> _consoleWriter = new();
        private readonly Mock<ICitySearchService> _citySearchService = new();

        public CityFinderAppTests()
        {
            _sut = new CityFinderApp(_consoleWriter.Object, _citySearchService.Object);
        }

        [Fact]
        public async void RunAsync_GivenCorrectArgs_ReturnsCity()
        {
            //Arrange
            string countryCode = "NL", postCode = "1000AC", city = "Amsterdam";

            var args = new[] { "-c", countryCode, "-p", postCode };
            CityResponse cityResponse = new() { City = city };
            CityRequest cityRequest = new() { CountyCode = countryCode, Postcode = postCode };

            _citySearchService.Setup(c => c.SearchByCountryAndPostCodeAsync(It.Is<CityRequest>(a => cityRequest.CountyCode.Equals(a.CountyCode) && cityRequest.Postcode.Equals(a.Postcode))).Result).Returns(cityResponse);//It.Is<CityRequest>(a => cityRequest.Equals(a))

            //Act
            await _sut.RunAsync(args);

            //Assert
            _consoleWriter.Verify(x => x.WriteLine($"The City you are looking for is probably: \"{city}\"."), Times.Exactly(1));
        }

        [Fact]
        public async void RunAsync_WritesCorrectMessage_WhenCityNotFoundExceptionIsThrown()
        {
            //Arrange
            string countryCode = "NL", postCode = "1000AC", city = "Amsterdam", exceptionMessage = "CityNotFoundException";

            var args = new[] { "-c", countryCode, "-p", postCode };
            CityResponse cityResponse = new() { City = city };
            CityRequest cityRequest = new() { CountyCode = countryCode, Postcode = postCode };

            _citySearchService.Setup(c => c.SearchByCountryAndPostCodeAsync(It.Is<CityRequest>(a => cityRequest.CountyCode.Equals(a.CountyCode) && cityRequest.Postcode.Equals(a.Postcode)))).Throws(new CityNotFoundException(exceptionMessage));//It.Is<CityRequest>(a => cityRequest.Equals(a))

            //Act
            await _sut.RunAsync(args);

            //Assert
            _consoleWriter.Verify(x => x.WriteLine(exceptionMessage), Times.Exactly(1));
        }
        [Fact]
        public async void RunAsync_WritesCorrectMessage_WhenCountryUnknownExceptionIsThrown()
        {
            //Arrange
            string countryCode = "NL", postCode = "1000AC", city = "Amsterdam", exceptionMessage = "CountryUnknownException";

            var args = new[] { "-c", countryCode, "-p", postCode };
            CityResponse cityResponse = new() { City = city };
            CityRequest cityRequest = new() { CountyCode = countryCode, Postcode = postCode };

            _citySearchService.Setup(c => c.SearchByCountryAndPostCodeAsync(It.Is<CityRequest>(a => cityRequest.CountyCode.Equals(a.CountyCode) && cityRequest.Postcode.Equals(a.Postcode)))).Throws(new CountryUnknownException(exceptionMessage));//It.Is<CityRequest>(a => cityRequest.Equals(a))

            //Act
            await _sut.RunAsync(args);

            //Assert
            _consoleWriter.Verify(x => x.WriteLine(exceptionMessage), Times.Exactly(1));
        }
        [Fact]
        public async void RunAsync_WritesCorrectMessage_WhenFailedToCallExceptionIsThrown()
        {
            //Arrange
            string countryCode = "NL", postCode = "1000AC", city = "Amsterdam", exceptionMessage = "FailedToCallException";

            var args = new[] { "-c", countryCode, "-p", postCode };
            CityResponse cityResponse = new() { City = city };
            CityRequest cityRequest = new() { CountyCode = countryCode, Postcode = postCode };

            _citySearchService.Setup(c => c.SearchByCountryAndPostCodeAsync(It.Is<CityRequest>(a => cityRequest.CountyCode.Equals(a.CountyCode) && cityRequest.Postcode.Equals(a.Postcode)))).Throws(new FailedToCallException(exceptionMessage));//It.Is<CityRequest>(a => cityRequest.Equals(a))

            //Act
            await _sut.RunAsync(args);

            //Assert
            _consoleWriter.Verify(x => x.WriteLine(exceptionMessage), Times.Exactly(1));
        }

    }
}
