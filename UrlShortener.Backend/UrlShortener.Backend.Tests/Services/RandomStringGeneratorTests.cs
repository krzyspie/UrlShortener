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
            randomNumberGeneratorMock.Generate(62).Returns(1);

            var service = new RandomStringGenerator(randomNumberGeneratorMock);

            // Act
            var result = service.Generate();

            // Assert
            Assert.Equal("bbbbbbbb", result);
            randomNumberGeneratorMock.Received(8).Generate(62);
        }
    }
}