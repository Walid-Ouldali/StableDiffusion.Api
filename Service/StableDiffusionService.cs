using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp.Formats.Png;
using StableDiffusion.ML.OnnxRuntime;

namespace StableDiffusion.Api.Service;

public static class StableDiffusionService
{
    public static async Task<byte[]> GenerateImage(string prompt, int numInferenceSteps, float guidanceScale)
    {
        var watch = System.Diagnostics.Stopwatch.StartNew();

        //Default args
        Console.WriteLine(prompt);

        var basePath = Directory.GetCurrentDirectory();
        var config = new StableDiffusionConfig
        {
            // Number of denoising steps
            NumInferenceSteps = numInferenceSteps,
            // Scale for classifier-free guidance
            GuidanceScale = guidanceScale,
            // The config is defaulted to CUDA. You can override it here if needed.
            // To use DirectML EP install the Microsoft.ML.OnnxRuntime.DirectML and uninstall Microsoft.ML.OnnxRuntime.GPU
            ExecutionProviderTarget = StableDiffusionConfig.ExecutionProvider.Cuda,

            // Update paths to your models
            TextEncoderOnnxPath = Path.Combine(basePath, "AiModel", "text_encoder", "model.onnx"),
            UnetOnnxPath = Path.Combine(basePath, "AiModel", "unet", "model.onnx"),
            VaeDecoderOnnxPath = Path.Combine(basePath, "AiModel", "vae_decoder", "model.onnx"),
            SafetyModelPath = Path.Combine(basePath, "AiModel", "safety_checker", "model.onnx"),
        };
        var image = UNet.Inference(prompt, config);
        using var memoryStream = new MemoryStream();
        await image.SaveAsync(memoryStream, new PngEncoder()); // Save image to memoryStream as PNG
        // Stop the timer
        watch.Stop();
        var elapsedMs = watch.ElapsedMilliseconds;
        Console.WriteLine("Time taken: " + elapsedMs + "ms");
        return memoryStream.ToArray();
    } 
}