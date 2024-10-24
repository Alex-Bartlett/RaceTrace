using Microsoft.Extensions.Logging;
using Moq;
using RaceLibrary.Common.Results;
using RaceLibrary.Models;
using RaceLibrary.Repositories;
using RaceLibrary.Services;

namespace UnitTests.RaceLibrary.Services
{
    public class RaceServiceTests
    {
        private readonly Mock<IRaceRepository> _raceRepositoryMock = new();
        private readonly Mock<ILogger<RaceService>> _loggerMock = new();
        private readonly RaceService _sut;

        public RaceServiceTests()
        {
            _sut = new(_raceRepositoryMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async void ReadAllFilesAsync_RacesFound_ShouldReturnRaces()
        {
            // Arrange
            var folderPath = "./";
            var raceStub = new Race { Name = "Race1", Laps = [] };
            var expected = new Race?[]
            {
                raceStub,
            };
            var raceLoadResultStub = new RaceLoadResult(expected, []);

            _raceRepositoryMock.Setup(r => r.GetAllRacesAsync(It.IsAny<string>())).ReturnsAsync(raceLoadResultStub);

            // Act
            var actual = await _sut.ReadAllFilesAsync(folderPath);
            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ReadAllFilesAsync_FolderPathInvalid_ShouldReturnEmptyAndLogError()
        {
            // Arrange
            var folderPath = "invalid";
            // Act
            Func<Task> actual = async () => await _sut.ReadAllFilesAsync(folderPath);
            // Assert
            Assert.ThrowsAsync<ArgumentException>(actual);
            _loggerMock.Verify(l => l.Log(
                It.Is<LogLevel>(e => e == LogLevel.Error),
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => true),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()), Times.Once);
        }

        [Fact]
        public async void ReadAllFilesAsync_FolderPathWhitespace_ShouldReturnEmptyAndLogError()
        {
            // Arrange
            var folderPath = " ";
            // Act
            var actual = await _sut.ReadAllFilesAsync(folderPath);
            // Assert
            Assert.Empty(actual);
            _loggerMock.Verify(l => l.Log(
                It.Is<LogLevel>(e => e == LogLevel.Error),
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => true),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()), Times.Once);
        }

        [Fact]
        public async void ReadAllFilesAsync_NullRaces_ShouldBeRemoved()
        {
            // Arrange
            var folderPath = "./";
            var raceStub = new Race { Name = "Race1", Laps = [] };
            var racesStub = new Race?[]
            {
                raceStub,
                null
            };
            var raceLoadResultStub = new RaceLoadResult(racesStub, []);

            _raceRepositoryMock.Setup(r => r.GetAllRacesAsync(It.IsAny<string>())).ReturnsAsync(raceLoadResultStub);

            var expected = new Race?[]
            {
                raceStub
            };
            // Act
            var actual = await _sut.ReadAllFilesAsync(folderPath);
            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async void ReadAllFilesAsync_ResultContainsErrors_ShouldLogErrors()
        {
            // Arrange
            var folderPath = "./";
            var exceptionStub = new Exception("Message");
            var exceptionsStub = new (string, Exception)[]
            {
                (folderPath, exceptionStub)
            };
            Race?[] expected = [];
            var raceLoadResultStub = new RaceLoadResult([], exceptionsStub);

            _raceRepositoryMock.Setup(r => r.GetAllRacesAsync(It.IsAny<string>())).ReturnsAsync(raceLoadResultStub);
            // Act
            var actual = await _sut.ReadAllFilesAsync(folderPath);
            // Assert
            Assert.Equal(expected, actual);
            _loggerMock.Verify(l => l.Log(
                It.Is<LogLevel>(e => e == LogLevel.Error),
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => true),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()), Times.Exactly(exceptionsStub.Length));
        }
    }
}
