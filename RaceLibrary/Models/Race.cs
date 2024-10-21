using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaceLibrary.Models
{
    public record Race
    {
        public required string Name { get; init; }
        public required IEnumerable<Lap> Laps { get; init; }
    }
}
