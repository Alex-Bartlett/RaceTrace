using RaceLibrary.Models;

namespace UnitTests.RaceLibrary.Models
{
    public class SectorTimeTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(4)]
        [InlineData(-1)]
        public void SectorTime_InvalidSectorNumber_ShouldThrowArgumentOutOfRangeException(int sectorNumber)
        {
            // Arrange
            var time = TimeSpan.FromSeconds(20);
            // Act
            Action actual = () => new SectorTime(sectorNumber, time);
            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(actual);
        }
    }
}
