using Application.Queries;
using Application.QueryHandlers;
using Infrastructure.Repository;
using NSubstitute;

namespace Application.Tests.QueryHanlders
{
    public class GetSourceUrlHandlerTests
    {
        [Fact]
        public async Task Should_ReturnSourceUrl_WhenShortcutExists()
        {
            // Arrange
            var originalUrl = "Test";
            var query = new GetSourceUrl()
            {
                ShortUrl = "test_url"
            };

            var urlRepositoryMock = Substitute.For<IUrlRepository>();
            urlRepositoryMock.GetShortUrl(query.ShortUrl).Returns(originalUrl);

            var handler = new GetSourceUrlHandler(urlRepositoryMock);

            // Act
            var result = await handler.Handle(query, default);

            // Assert
            Assert.Equal(originalUrl, result);

            urlRepositoryMock.Received(1).GetShortUrl(query.ShortUrl);
        }
    }
}
