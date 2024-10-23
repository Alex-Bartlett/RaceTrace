using Microsoft.Extensions.Logging;
using Moq;
using RaceLibrary.DataTools;
using RaceLibrary.Models;

namespace UnitTests.RaceLibrary.DataTools
{
    public class RaceDataReaderTests
    {
        private readonly Mock<ILogger<RaceDataReader>> _loggerMock = new();

        [Fact]
        public async void TestRead()
        {
            // Arrange
            var folderPath = "C:\\Users\\ABBARTLETT\\Documents\\Projects\\RaceTrace\\Data";
            var sut = new RaceDataReader(folderPath, _loggerMock.Object);
            // Act
            var actual = await sut.ReadAllFilesAsync();
            // Assert
            Assert.NotEmpty(actual);
        }

        [Fact]
        public void RaceDataReader_FolderPathInvalid_ShouldThrowArgumentException()
        {
            // Arrange
            var folderPath = "invalid";
            // Act
            Action actual = () => new RaceDataReader(folderPath, _loggerMock.Object);
            // Assert
            Assert.Throws<ArgumentException>(actual);
        }
    }
}
