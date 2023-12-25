using System.Security.Cryptography;
using LocationFinder.Models;
using LocationFinder.Service;
using LocationFinder.Settings;
using Microsoft.AspNetCore.Mvc;

namespace LocationFinder.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LocationFindController(ILocationFinderService locationFinderService) : ControllerBase
{
    [HttpGet]
    public async ValueTask<IActionResult> FindLocationAsync(
        [FromQuery] decimal latitude,
        [FromQuery] decimal longitude)
    {
        var result =
            await locationFinderService.FindAsync(latitude, longitude);
   
        return Ok(result);
    }
}