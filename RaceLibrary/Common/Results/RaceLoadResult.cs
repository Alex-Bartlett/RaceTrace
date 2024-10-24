using RaceLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaceLibrary.Common.Results
{
    public record RaceLoadResult(
        IReadOnlyList<Race?> Races,
        IReadOnlyList<(string filePath, Exception exception)> Errors
    );
}
