using RaceLibrary.Helpers;
using RaceLibrary.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.RaceLibrary.Helpers
{
    public class FileHelperTests
    {

        [Fact]
        public void GetFilesWithExtension_InvalidDirectory_ShouldThrowArgumentException()
        {
            // Arrange
            var directoryPath = " ";
            var fileExtension = ".txt";

            // Act
            Action actual = () => FileHelper.GetFilesWithExtension(directoryPath, fileExtension);

            // Assert
            Assert.Throws<ArgumentException>(actual);
        }

        [Fact]
        public void GetFilesWithExtension_InvalidDirectory_ShouldThrowDirectoryNotFoundException()
        {
            // Arrange
            var directoryPath = "Z:\\INVALID_DIRECTORY";
            var fileExtension = ".txt";

            // Act
            Action actual = () => FileHelper.GetFilesWithExtension(directoryPath, fileExtension);

            // Assert
            Assert.Throws<DirectoryNotFoundException>(actual);
        }
    }
}
