using Microsoft.AspNetCore.Mvc;

namespace SocialNetwork.API.MVC.Controllers;

public class UtilitiesController : Controller
{
    public async Task<IActionResult> ProxyImage(
        [FromQuery] string url)
    {
        using var httpClient = new HttpClient();
        var response = await httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode)
            return StatusCode((int)response.StatusCode);

        var contentType = response.Content.Headers.ContentType?.MediaType ?? "image/jpeg";
        var imageBytes = await response.Content.ReadAsByteArrayAsync();
        return File(imageBytes, contentType);
    }
}
