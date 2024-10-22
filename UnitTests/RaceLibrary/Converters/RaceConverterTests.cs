using RaceLibrary.Converters;
using RaceLibrary.Models;
using System.Text.Json;

namespace UnitTests.RaceLibrary.Converters
{
    public class RaceConverterTests
    {
        private readonly JsonSerializerOptions _options;

        public RaceConverterTests()
        {
            _options = new JsonSerializerOptions
            {
                Converters = { new RaceConverter() }
            };
        }

        [Fact]
        public void Read_ValidJson_ShouldDeserializeSuccessfully()
        {
            // Arrange
            var json = """
                {
                    "MRData": {
                        "RaceTable": {
                            "Races": [
                                {
                                    "raceName": "TestRace",
                                    "Laps": [
                                        {
                                            "number": 1,
                                            "Timings": [
                                                {
                                                    "driverId": "test_driver1",
                                                    "position": 1,
                                                    "time": "1:23.456"
                                                },
                                                {
                                                    "driverId": "test_driver2",
                                                    "position": 2,
                                                    "time": "1:56.789"
                                                }
                                            ]
                                        },
                                        {
                                            "number": 2,
                                            "Timings": [
                                                {
                                                    "driverId": "test_driver1",
                                                    "position": 1,
                                                    "time": "0:59.999"
                                                },
                                                {
                                                    "driverId": "test_driver2",
                                                    "position": 2,
                                                    "time": "1:00.000"
                                                }
                                            ]
                                        }
                                    ]
                                }
                            ]
                        }
                    }
                }
                """;

            var expectedRace = new Race
            {
                Name = "TestRace",
                Laps = new List<Lap>()
                {
                    new
                    (
                        "test_driver1",
                        1,
                        1,
                        new LapTime (ParseTimeSpan("1:23.456"))
                    ),
                    new
                    (
                        "test_driver2",
                        2,
                        1,
                        new LapTime (ParseTimeSpan("1:56.789"))
                    ),
                    new
                    (
                        "test_driver1",
                        1,
                        2,
                        new LapTime (ParseTimeSpan("0:59.999"))
                    ),
                    new
                    (
                        "test_driver2",
                        2,
                        2,
                        new LapTime (ParseTimeSpan("1:00.000"))
                    )
                }
            };

            // Act
            var actual = JsonSerializer.Deserialize<Race>(json, _options);
            // Assert
            Assert.NotNull(actual);
            Assert.Equal(expectedRace.Name, actual.Name);
            Assert.Equal(expectedRace.Laps, actual.Laps);

        }

        [Fact]
        public void Read_NoRaces_ShouldThrowJsonException()
        {
            // Arrange
            var json = """
                {
                    "MRData": {
                        "RaceTable": {
                            "Races": [
                            ]
                        }
                    }
                }
                """;

            // Act
            Action actual = () => JsonSerializer.Deserialize<Race>(json, _options);
            // Assert
            Assert.Throws<JsonException>(actual);
        }

        [Theory]
        [InlineData("{\"MRData\": null}")]
        [InlineData("{\"MRData\": {\"RaceTable\": null}}")]
        [InlineData("{\"MRData\": {\"RaceTable\": {\"Races\": null}}}")]
        [InlineData("{\"MRData\": {\"RaceTable\": {\"Races\": [{\"raceName\": null,\"Laps\": [{\"number\": 1,\"Timings\": [{\"driverId\": \"test_driver1\",\"position\": 1,\"time\": \"1:23.456\"}]}]}]}}}")]
        [InlineData("{\"MRData\": {\"RaceTable\": {\"Races\": [{\"raceName\": \"TestRace\",\"Laps\": null}]}}}")]
        public void Read_RequiredValueNull_ShouldThrowJsonException(string json)
        {
            // Arrange

            // Act
            Action actual = () => JsonSerializer.Deserialize<Race>(json, _options);
            // Assert
            Assert.Throws<JsonException>(actual);
        }

        [Fact]
        public void Read_RaceNameMissing_ShouldThrowJsonException()
        {
            // Arrange
            var json = "{\"MRData\": {\"RaceTable\": {\"Races\": [{\"Laps\": [{\"number\": 1,\"Timings\": [{\"driverId\": \"test_driver1\",\"position\": 1,\"time\": \"1:23.456\"}]}]}]}}}";

            // Act
            Action actual = () => JsonSerializer.Deserialize<Race>(json, _options);
            // Assert
            Assert.Throws<JsonException>(actual);
        }

        [Fact]
        public void Read_LapTimeFormatInvalid_ShouldThrowFormatException()
        {
            // Arrange
            var json = """
                {
                    "MRData": {
                        "RaceTable": {
                            "Races": [
                                {
                                    "raceName": "TestRace",
                                    "Laps": [
                                        {
                                            "number": 1,
                                            "Timings": [
                                                {
                                                    "driverId": "test_driver1",
                                                    "position": 1,
                                                    "time": "1:23:45.67"
                                                }
                                            ]
                                        }
                                    ]
                                }
                            ]
                        }
                    }
                }
                """;

            // Act
            Action action = () => JsonSerializer.Deserialize<Race>(json, _options);
            // Assert
            Assert.Throws<FormatException>(action);
        }

        // Helper method to convert time from format in JSON to TimeSpan
        private TimeSpan ParseTimeSpan(string time)
        {
            return TimeSpan.ParseExact(time, @"m\:ss\.fff", null);
        }
    }
}
