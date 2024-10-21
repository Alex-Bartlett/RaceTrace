using RaceLibrary.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RaceLibrary.Converters
{
    public class RaceConverter : JsonConverter<Race>
    {
        public override Race Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using var jsonDocument = JsonDocument.ParseValue(ref reader);

            // TODO: Update this to go MRData -> RaceTable -> Races
            var races = GetRequiredProperty(jsonDocument.RootElement, "Races").EnumerateArray();
            // Expect only one race. With current data, this is correct, but may be subject to change in the future.
            var racesCount = races.Count();
            if (racesCount != 1)
            {
                throw new JsonException(string.Format("Property 'Races' must contain exactly one element, found ", racesCount));
            }
            // Get the first race in the races property - the desired root for this deserialization
            var root = races.First();
            return MapRace(root);

        }

        private static Race MapRace(JsonElement root)
        {
            return new Race()
            {
                Name = GetRequiredProperty(root, "raceName").GetString()!,
                Laps = MapLaps(root),
            };
        }

        private static IEnumerable<Lap> MapLaps(JsonElement root)
        {
            List<Lap> laps = [];
            var lapElements = GetRequiredProperty(root, "Laps").EnumerateArray();
            foreach (var lapElement in lapElements)
            {
                try
                {
                    var lapNumber = GetRequiredProperty(lapElement, "number").GetInt16();
                    var timingsElements = GetRequiredProperty(lapElement, "Timings").EnumerateArray();
                    foreach (var timingsElement in timingsElements)
                    {
                        Lap lap = new(
                            GetRequiredProperty(timingsElement, "driverId").GetString()!,
                            lapNumber,
                            GetRequiredProperty(timingsElement, "position").GetInt16(),
                            MapLapTime(timingsElement)
                        );
                        laps.Add(lap);
                    }
                }
                catch (JsonException ex)
                {
                    // Skip laps with invalid data - this may not be the desired behaviour
                    Console.Error.WriteLine("Lap missing required property, skipping. \n{0}", ex.Message);
                    continue;
                }
            }
            return laps;
        }

        private static LapTime MapLapTime(JsonElement root)
        {
            // If sector support is added, this will need expanding.

            if (TimeSpan.TryParseExact(GetRequiredProperty(root, "time").GetString(), @"m\:ss\.fff", null, out var total)) {
                return new LapTime(total);
            }
            else
            {
                throw new JsonException("Invalid time format");
            }
        }

        private static JsonElement GetRequiredProperty(JsonElement root, string propertyName)
        {
            if (root.TryGetProperty(propertyName, out var property))
            {
                return property;
            }
            else
            {
                throw new JsonException(string.Format("JSON missing required property {0}", propertyName));
            }
        }


        public override void Write(Utf8JsonWriter writer, Race value, JsonSerializerOptions options)
        {
            // Write not needed for this project, but can be added later when needed.
            throw new NotImplementedException();
        }
    }
}