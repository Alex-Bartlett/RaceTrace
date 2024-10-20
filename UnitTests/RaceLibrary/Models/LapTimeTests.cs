using RaceLibrary.Models;

namespace UnitTests.RaceLibrary.Models
{
    public class LapTimeTests
    {
        [Fact]
        public void LapTime_CumulativeSectorTimesMatchTotal_ShouldCreateLapTimeSuccessfully()
        {
            // Arrange
            var sectorTimes = new SectorTime[]
            {
                new SectorTime(1, TimeSpan.FromSeconds(20)),
                new SectorTime(2, TimeSpan.FromSeconds(20)),
                new SectorTime(3, TimeSpan.FromSeconds(20))
            };
            var totalTime = TimeSpan.FromSeconds(60);
            // Act
            var actual = new LapTime(totalTime, sectorTimes);
            // Assert
            Assert.NotNull(actual);
        }

        [Fact]
        public void LapTime_CumulativeSectorTimesDontMatchTotal_ShouldThrowArgumentException()
        {
            // Arrange
            var sectorTimes = new SectorTime[]
            {
                new SectorTime(1, TimeSpan.FromSeconds(20)),
                new SectorTime(2, TimeSpan.FromSeconds(20)),
                new SectorTime(3, TimeSpan.FromSeconds(20))
            };
            var totalTime = TimeSpan.Zero;
            // Act
            Action actual = () => new LapTime(totalTime, sectorTimes);
            // Assert
            Assert.Throws<ArgumentException>(actual);
        }

        [Fact]
        public void LapTime_OnlySectorTimesProvided_ShouldCalculateTotal()
        {
            // Arrange
            var sectorTimes = new SectorTime[]
            {
                new SectorTime(1, TimeSpan.FromSeconds(20)),
                new SectorTime(2, TimeSpan.FromSeconds(20)),
                new SectorTime(3, TimeSpan.FromSeconds(20))
            };
            var expectedTotal = TimeSpan.FromSeconds(60);
            // Act
            var actual = new LapTime(sectorTimes);
            // Assert
            Assert.Equal(expectedTotal, actual.Total);
        }

        [Fact]
        public void LapTime_OnlyTotalProvided_ShouldSetSectorTimesToNull()
        {
            // Arrange
            var totalTime = TimeSpan.FromSeconds(60);
            // Act
            var actual = new LapTime(totalTime);
            // Assert
            Assert.Null(actual.SectorTimes);
        }
    }
}