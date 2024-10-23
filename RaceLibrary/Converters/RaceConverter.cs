using RaceLibrary.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RaceLibrary.Converters
{
    public class RaceConverter : JsonConverter<Race>
    {
        /// <summary>
        /// Deserializes a JSON file into a Race object
        /// </summary>
        public override Race Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using var jsonDocument = JsonDocument.ParseValue(ref reader);

            var raceElement = FindRaceInJson(jsonDocument.RootElement);
            
            return MapRace(raceElement);

        }

        /// <summary>
        /// Traverses a race JSON document to find the 'Races' element.
        /// </summary>
        private static JsonElement FindRaceInJson(JsonElement root)
        {
            // Navigate the JSON file to find the Races array
            var mrdata = GetRequiredProperty(root, "MRData");
            var raceTable = GetRequiredProperty(mrdata, "RaceTable");
            var races = GetRequiredProperty(raceTable, "Races").EnumerateArray();
            // Expect only one race. With current data, this is correct, but may be subject to change in the future.
            var racesCount = races.Count();
            if (racesCount != 1)
            {
                throw new JsonException(string.Format("Property 'Races' must contain exactly one element, found {0}", racesCount));
            }
            return races.First();
        }

        /// <summary>
        /// Maps a JSON element to a Race object
        /// </summary>
        private static Race MapRace(JsonElement root)
        {
            return new Race()
            {
                Name = GetRequiredProperty(root, "raceName").GetString()!,
                Laps = MapLaps(root),
            };
        }

        /// <summary>
        /// Maps the 'Laps' array of a JSON element to a collection of Lap objects.
        /// </summary>
        private static IEnumerable<Lap> MapLaps(JsonElement root)
        {
            List<Lap> laps = [];
            var lapElements = GetRequiredProperty(root, "Laps").EnumerateArray();
            foreach (var lapElement in lapElements)
            {
                var lapNumber = short.Parse(GetRequiredProperty(lapElement, "number").GetString()!);
                var timingsElements = GetRequiredProperty(lapElement, "Timings").EnumerateArray();
                foreach (var timingsElement in timingsElements)
                {
                    Lap lap = new(
                        GetRequiredProperty(timingsElement, "driverId").GetString()!,
                        short.Parse(GetRequiredProperty(timingsElement, "position").GetString()!),
                        lapNumber,
                        MapLapTime(timingsElement)
                    );
                    laps.Add(lap);
                }
            }
            return laps;
        }

        /// <summary>
        /// Maps the 'time' property of a JSON element to a LapTIme object.
        /// </summary>
        private static LapTime MapLapTime(JsonElement root)
        {
            // If sector support is added, this will need expanding.

            if (TimeSpan.TryParseExact(GetRequiredProperty(root, "time").GetString(), @"m\:ss\.fff", null, out var total)) {
                return new LapTime(total);
            }
            else
            {
                throw new FormatException("Invalid time format");
            }
        }

        /// <summary>
        /// Gets a property within a JSON element, returning the property if found or throwing an exception if not.
        /// </summary>
        /// <param name="root">JSON element to search within</param>
        /// <param name="propertyName">Property name (case sensitive)</param>
        /// <returns>The JSON element for the property found</returns>
        /// <exception cref="JsonException">Thrown if the property is missing or null</exception>
        private static JsonElement GetRequiredProperty(JsonElement root, string propertyName)
        {
            if (root.TryGetProperty(propertyName, out var property)
                && property.ValueKind != JsonValueKind.Null)
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