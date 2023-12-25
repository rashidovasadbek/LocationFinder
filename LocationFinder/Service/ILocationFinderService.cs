using LocationFinder.Models;
using LocationFinder.Settings;

namespace LocationFinder.Service;

public interface ILocationFinderService
{
    ValueTask<GeoLocation> FindAsync(decimal latitude, decimal longitude);
}