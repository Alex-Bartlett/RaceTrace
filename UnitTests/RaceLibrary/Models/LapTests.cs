using RaceLibrary.Models;

namespace UnitTests.RaceLibrary.Models
{
    public class LapTests
    {
        [Fact]
        public void Lap_ValidPositionAndLapNumber_ShouldCreateLapSuccessfully()
        {
            // Arrange
            var driverId = "driver";
            short position = 1;
            short lapNumber = 1;
            var lapTime = new LapTime(TimeSpan.Zero);
            // Act
            var actual = new Lap(driverId, position, lapNumber, lapTime);
            // Assert
            Assert.NotNull(actual);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void Lap_InvalidPosition_ShouldThrowArgumentOutOfRangeException(short position)
        {
            // Arrange
            var driverId = "driver";
            short lapNumber = 1;
            var lapTime = new LapTime(TimeSpan.Zero);
            // Act
            Action actual = () => new Lap(driverId, position, lapNumber, lapTime);
            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(actual);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void Lap_InvalidLapNumber_ShouldThrowArgumentOutOfRangeException(short lapNumber)
        {
            // Arrange
            var driverId = "driver";
            short position = 1;
            var lapTime = new LapTime(TimeSpan.Zero);
            // Act
            Action actual = () => new Lap(driverId, position, lapNumber, lapTime);
            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(actual);
        }
    }
}
