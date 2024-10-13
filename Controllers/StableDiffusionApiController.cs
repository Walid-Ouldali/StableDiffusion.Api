using Microsoft.AspNetCore.Mvc;

namespace StableDiffusion.Api.Controllers;

[ApiController]
[Route("/")]
public class StableDiffusionApiController : ControllerBase
{
    
    [HttpGet]
    [Route("image")]
    public IActionResult GenerateImage() {
        return Ok("You're on your own kid!");
    }
}