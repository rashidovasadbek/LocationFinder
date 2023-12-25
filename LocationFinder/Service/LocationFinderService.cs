using System.Web;
using LocationFinder.Models;
using LocationFinder.Settings;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace LocationFinder.Service;

public class LocationFinder
{
    private readonly LocationFinderSettings _locationFinderSettings;
    public HttpClient HttpClient { get; init; }

    public LocationFinder(IOptions<LocationFinderSettings> locationFinderSettings, IHttpClientFactory httpClientFactory)
    {
        HttpClient = httpClientFactory.CreateClient();
        _locationFinderSettings = locationFinderSettings.Value;
        // Set base addresss
        HttpClient.BaseAddress = new Uri(locationFinderSettings.Value.BaseAddressUrl);
    }


    public async ValueTask<GeoLocation> FindAsync(decimal latitude, decimal longitude)
    {
        // Create query paramters
        var queryParameters = HttpUtility.ParseQueryString(string.Empty);
        queryParameters["latitude"] = latitude.ToString();
        queryParameters["longitude"] = longitude.ToString();

        // Add query parameters to URL
        var url = $"{_locationFinderSettings.LocationsUrl}?{queryParameters}";

        // Create http message
        var request = new HttpRequestMessage(HttpMethod.Get, url);

        // Send request 
        var response = await HttpClient.SendAsync(request);

        if (response.IsSuccessStatusCode)
        {
            // Map response
            var content = response.Content.ReadAsStringAsync();
            var geoLocation = JsonConvert.DeserializeObject<GeoLocation>(await content);

            return geoLocation ?? throw new ArgumentNullException();
        }
        else
        {
            throw new ArgumentException();
        }
        
    }
}