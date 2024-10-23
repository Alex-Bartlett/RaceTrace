using Microsoft.Extensions.Logging;
using Moq;
using RaceLibrary.Models;
using RaceLibrary.Repositories;
using RaceLibrary.Services;

namespace UnitTests.RaceLibrary.Services
{
    public class RaceServiceTests
    {
        private readonly Mock<RaceRepository> _raceRepositoryMock = new();
        private readonly Mock<ILogger<RaceService>> _loggerMock = new();
        private readonly RaceService _sut;

        public RaceServiceTests()
        {
            _sut = new("./", _raceRepositoryMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async void TestRead()
        {
            // Arrange
            var folderPath = "C:\\Users\\ABBARTLETT\\Documents\\Projects\\RaceTrace\\Data";
            _sut.DataDirectory = folderPath;
            // Act
            var actual = await _sut.ReadAllFilesAsync();
            // Assert
            Assert.NotEmpty(actual);
        }

        [Fact]
        public void ReadAllFilesAsync_FolderPathInvalid_ShouldThrowArgumentException()
        {
            // Arrange
            var folderPath = "invalid";
            _sut.DataDirectory = folderPath;
            // Act
            Func<Task> actual = async () => await _sut.ReadAllFilesAsync();
            // Assert
            Assert.ThrowsAsync<ArgumentException>(actual);
        }

        [Fact]
        public async void ReadAllFilesAsync_FolderPathWhitespace_ShouldThrowArgumentException()
        {
            // Arrange
            var folderPath = " ";
            _sut.DataDirectory = folderPath;
            // Act
            Func<Task> actual = async () => await _sut.ReadAllFilesAsync();
            // Assert
            // Verify it is logging HERE
            Assert.ThrowsAsync<ArgumentException>(actual); // This passes for any error type?
        }
    }
}
