using RaceLibrary.DataTools;
using RaceLibrary.Models;

namespace UnitTests.RaceLibrary.DataTools
{
    public class DataReaderTests
    {
        [Fact]
        public async void TestRead()
        {
            // Arrange
            var folderPath = "C:\\Users\\ABBARTLETT\\Documents\\Projects\\RaceTrace-DataReader\\Data";
            var sut = new DataReader(folderPath);
            // Act
            var result = await sut.ReadAllFiles();
            // Assert
            Assert.NotEmpty(result);
        }
    }
}
