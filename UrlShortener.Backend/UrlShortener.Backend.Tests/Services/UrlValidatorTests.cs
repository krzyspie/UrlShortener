using Application.Services;

namespace Application.Tests.Services
{
    public class UrlValidatorTests
    {
        [Fact]
        public void ValidUrlTest()
        {
            // Arrange
            var validator = new UrlValidator();

            // Act
            var result = validator.IsValid("https://example.com/");

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void InvalidUrlTest()
        {
            // Arrange
            var validator = new UrlValidator();

            // Act
            var result = validator.IsValid("example");

            // Assert
            Assert.False(result);
        }
    }
}