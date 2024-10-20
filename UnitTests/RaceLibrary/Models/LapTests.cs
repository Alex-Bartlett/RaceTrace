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
            var position = 1;
            var lapNumber = 1;
            var lapTime = new LapTime(TimeSpan.Zero);
            // Act
            var actual = new Lap(driverId, position, lapNumber, lapTime);
            // Assert
            Assert.NotNull(actual);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void Lap_InvalidPosition_ShouldThrowArgumentOutOfRangeException(int position)
        {
            // Arrange
            var driverId = "driver";
            var lapNumber = 1;
            var lapTime = new LapTime(TimeSpan.Zero);
            // Act
            Action actual = () => new Lap(driverId, position, lapNumber, lapTime);
            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(actual);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void Lap_InvalidLapNumber_ShouldThrowArgumentOutOfRangeException(int lapNumber)
        {
            // Arrange
            var driverId = "driver";
            var position = 1;
            var lapTime = new LapTime(TimeSpan.Zero);
            // Act
            Action actual = () => new Lap(driverId, position, lapNumber, lapTime);
            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(actual);
        }
    }
}
