using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaceLibrary.Helpers
{
    public static class FileHelper
    {
        /// <summary>
        /// Gets the paths of all files in a directory with the specified extension.
        /// </summary>
        /// <param name="directoryPath">Directory to search</param>
        /// <param name="fileExtension">File extension of files to return, e.g. '.csv'</param>
        /// <returns>A collection of file paths within the directory</returns>
        public static IEnumerable<string> GetFilesWithExtension(string directoryPath, string fileExtension)
        {
            // Add a dot to the file extension if it doesn't already have one
            if (!fileExtension.StartsWith('.'))
            {
                fileExtension = '.' + fileExtension;
            }
            return Directory.EnumerateFiles(directoryPath).Where(f => f.EndsWith(fileExtension));
        }

        /// <summary>
        /// Checks if a directory exists and path
        /// </summary>
        /// <param name="directoryPath"></param>
        /// <param name="invalidReason"></param>
        /// <returns></returns>
        public static bool IsValidDirectory(string directoryPath, out string? invalidReason)
        {
            invalidReason = null;
            if (string.IsNullOrWhiteSpace(directoryPath))
            {
                invalidReason = $"{nameof(directoryPath)} is null or empty";
            }
            if (!Directory.Exists(directoryPath))
            {
                invalidReason = $"Directory does not exist: {directoryPath}";
                return false;
            }
            return true;
        }
    }
}
