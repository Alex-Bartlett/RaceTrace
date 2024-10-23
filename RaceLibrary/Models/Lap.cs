namespace RaceLibrary.Models
{
    public record class Lap
    {
        public string DriverId { get; init; }
        // Position not relevant to exercise, but included for future development
        public short Position { get; init; }
        public short LapNumber { get; init; }
        public LapTime LapTime { get; init; }

        public Lap(string driverId, short position, short lapNumber, LapTime lapTime)
        {
            if (position < 1) throw new ArgumentOutOfRangeException(nameof(position), "Position must be greater than 0");
            if (lapNumber < 1) throw new ArgumentOutOfRangeException(nameof(lapNumber), "Lap number must be greater than 0");
            DriverId = driverId;
            Position = position;
            LapNumber = lapNumber;
            LapTime = lapTime;
        }
    }
}
