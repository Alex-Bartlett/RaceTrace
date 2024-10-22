using RaceLibrary.DataTools;
using RaceLibrary.Models;

namespace UnitTests.RaceLibrary.DataTools
{
    public class RaceDataReaderTests
    {
        [Fact]
        public async void TestRead()
        {
            // Arrange
            var folderPath = "C:\\Users\\ABBARTLETT\\Documents\\Projects\\RaceTrace\\Data";
            IRaceDataReader sut = new RaceDataReader(folderPath);
            // Act
            var result = await sut.ReadAllFiles();
            // Assert
            Assert.NotEmpty(result);
        }
    }
}
