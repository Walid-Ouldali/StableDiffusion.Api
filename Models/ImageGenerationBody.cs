namespace StableDiffusion.Api.Models;

public class ImageGenerationBody
{
    public string Prompt { get; set; }
    
    public int NumInferenceSteps { get; set; }
    
    public float GuidanceScale { get; set; }
    
}

