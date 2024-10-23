using Microsoft.Extensions.Logging;
using Moq;
using RaceLibrary.Models;
using RaceLibrary.Services;

namespace UnitTests.RaceLibrary.Services
{
    public class RaceServiceTests
    {
        private readonly Mock<ILogger<RaceService>> _loggerMock = new();

        [Fact]
        public async void TestRead()
        {
            // Arrange
            var folderPath = "C:\\Users\\ABBARTLETT\\Documents\\Projects\\RaceTrace\\Data";
            var sut = new RaceService(folderPath, _loggerMock.Object);
            // Act
            var actual = await sut.ReadAllFilesAsync();
            // Assert
            Assert.NotEmpty(actual);
        }

        [Fact]
        public void RaceService_FolderPathInvalid_ShouldThrowArgumentException()
        {
            // Arrange
            var folderPath = "invalid";
            // Act
            Action actual = () => new RaceService(folderPath, _loggerMock.Object);
            // Assert
            Assert.Throws<ArgumentException>(actual);
        }

        [Fact]
        public void RaceService_FolderPathWhitespace_ShouldThrowArgumentException()
        {
            // Arrange
            var folderPath = " ";
            // Act
            Action actual = () => new RaceService(folderPath, _loggerMock.Object);
            // Assert
            Assert.Throws<ArgumentException>(actual);
        }
    }
}
