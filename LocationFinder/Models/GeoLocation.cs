using System.Text.Json.Serialization;

namespace LocationFinder.Models;

public class GeoLocation
{
    [JsonPropertyName("")] public string CountryName { get; set; } = default!;

    [JsonPropertyName("")] public string CountryCode { get; set; } = default!;
}