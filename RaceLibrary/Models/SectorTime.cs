using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaceLibrary.Models
{
    // Include sector times for future development
    public readonly record struct SectorTime
    {
        public int Number { get; init; }
        public TimeSpan Time { get; init; }

        public SectorTime(int number, TimeSpan time)
        {
            if (number < 1 || number > 3)
            {
                throw new ArgumentOutOfRangeException(nameof(number), "Sector number must be between 1 and 3");
            }

            Number = number;
            Time = time;
        }
    }
}
