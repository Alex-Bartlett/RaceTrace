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
                    "Data": {
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
                Laps = new Lap[]
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
                        2,
                        1,
                        new LapTime (ParseTimeSpan("0:59.999"))
                    ),
                    new
                    (
                        "test_driver2",
                        2,
                        2,
                        new LapTime (ParseTimeSpan("1:00.000"))
                    ),
                }
            };

            // Act
            var actual = JsonSerializer.Deserialize<Race>(json, _options);
            // Assert
            Assert.Equal(expectedRace, actual);

        }

        [Fact]
        public void Read_NoRaces_ShouldThrowJsonException()
        {
            // Arrange
            // Act
            // Assert
        }

        [Fact]
        public void Read_InvalidLapObject_ShouldSkipLapAndLogError()
        {
            // Arrange
            // Act
            // Assert
        }

        [Fact]
        public void Read_RaceNameMissing_ShouldThrowJsonException()
        {
            // Arrange
            // Act
            // Assert
        }

        [Fact]
        public void Read_LapTimeFormatInvalid_ShouldThrowJsonException()
        {
            // Arrange
            // Act
            // Assert
        }

        // Helper method to convert time from format in JSON to TimeSpan
        private TimeSpan ParseTimeSpan(string time)
        {
            return TimeSpan.ParseExact(time, @"m\:ss\.fff", null);
        }
    }
}
