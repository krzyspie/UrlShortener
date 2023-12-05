using Application.CommandHandlers;
using Application.Commands;
using Application.Interfaces;
using Infrastructure.Repository;
using NSubstitute;

namespace Application.Tests.CommandHandlers
{
    public class CreateShortUrlHandlerTests
    {
        [Fact]
        public async Task Should_ReturnExistingShortcut_WhenShortcutExists()
        {
            // Arrange
            var existingShortcut = "Test";
            var command = new CreateShortUrl()
            {
                OriginUrl = "test_url"
            };
            var randomStringGeneratorMock = Substitute.For<IRandomStringGenerator>();
            var urlRepositoryMock = Substitute.For<IUrlRepository>();
            urlRepositoryMock.GetShortUrl(command.OriginUrl).Returns(existingShortcut);

            var handler = new CreateShortUrlHandler(randomStringGeneratorMock, urlRepositoryMock);

            // Act
            var result = await handler.Handle(command, default);

            // Assert
            Assert.Equal(existingShortcut, result);

            urlRepositoryMock.Received(1).GetShortUrl(command.OriginUrl);
            randomStringGeneratorMock.DidNotReceive().Generate();
            urlRepositoryMock.DidNotReceive().SaveShortUrl(Arg.Any<string>(), Arg.Any<string>());
        }

        [Fact]
        public async Task Should_ReturnGeneratedShortcut_WhenShortcutNotExist()
        {
            // Arrange
            var generatedShortcut = "New_Shortcut";
            var command = new CreateShortUrl()
            {
                OriginUrl = "test_url"
            };
            var randomStringGeneratorMock = Substitute.For<IRandomStringGenerator>();
            var urlRepositoryMock = Substitute.For<IUrlRepository>();
            urlRepositoryMock.GetShortUrl(command.OriginUrl).Returns(string.Empty);
            randomStringGeneratorMock.Generate().Returns(generatedShortcut);

            var handler = new CreateShortUrlHandler(randomStringGeneratorMock, urlRepositoryMock);

            // Act
            var result = await handler.Handle(command, default);

            // Assert
            Assert.Equal(generatedShortcut, result);
            urlRepositoryMock.Received(1).GetShortUrl(command.OriginUrl);
            randomStringGeneratorMock.Received(1).Generate();
            urlRepositoryMock.Received(1).SaveShortUrl(command.OriginUrl, generatedShortcut);
        }
    }
}
