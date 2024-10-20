namespace RaceLibrary.Models
{
    public record class LapTime
    {
        public SectorTime[]? SectorTimes { get; init; }
        public TimeSpan Total { get; init; }

        public LapTime(TimeSpan total, SectorTime[] sectorTimes)
        {
            var calculatedTotal = CalculateTotal(sectorTimes);
            if (total != calculatedTotal)
            {
                throw new ArgumentException("Total time does not match sum of sector times", nameof(total));
            }
        }

        public LapTime(TimeSpan total)
        {
            Total = total;
        }

        public LapTime(SectorTime[] sectorTimes)
        {
            Total = CalculateTotal(sectorTimes);
            SectorTimes = sectorTimes;
        }

        private static TimeSpan CalculateTotal(SectorTime[] sectorTimes)
        {
            TimeSpan total = TimeSpan.Zero;
            foreach (var sector in sectorTimes)
            {
                total += sector.Time;
            }
            return total;
        }
    }
}
