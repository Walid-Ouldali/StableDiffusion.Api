using Microsoft.AspNetCore.Mvc;
using StableDiffusion.Api.Models;
using StableDiffusion.Api.Service;

namespace StableDiffusion.Api.Controllers;

[ApiController]
[Route("/")]
public class StableDiffusionApiController : ControllerBase
{
    private readonly StableDiffusionService _imageGenerationService = new();

    [HttpPost]
    [Route("image")]
    public async Task<IActionResult> GenerateImage([FromBody] ImageGenerationBody generationBody) {
        var image = await _imageGenerationService.GenerateImage(generationBody.Prompt, generationBody.NumInferenceSteps, generationBody.GuidanceScale);
        return File(image, "image/png", "generated_image.png");
    }
}