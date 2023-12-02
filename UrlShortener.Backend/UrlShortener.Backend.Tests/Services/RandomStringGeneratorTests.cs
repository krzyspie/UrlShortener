using Application.Interfaces;
using Application.Services;
using NSubstitute;

namespace Application.Tests.Services
{
    public class RandomStringGeneratorTests
    {
        [Fact]
        public void GenerateStringTest()
        {
            // Arrange
            var randomNumberGeneratorMock = Substitute.For<IRandomNumberGenerator>();
            randomNumberGeneratorMock.Generate(61).Returns(60);

            var service = new RandomStringGenerator(randomNumberGeneratorMock);

            // Act
            var result = service.Generate();

            // Assert
            Assert.Equal("99999999", result);
            randomNumberGeneratorMock.Received(8).Generate(61);
        }
    }
}