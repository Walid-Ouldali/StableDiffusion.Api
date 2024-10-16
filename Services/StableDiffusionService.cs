﻿using SixLabors.ImageSharp.Formats.Png;
using StableDiffusion.ML.OnnxRuntime;

namespace StableDiffusion.Api.Services;

public class StableDiffusionService
{
    public async Task<byte[]> GenerateImage(string prompt, int numInferenceSteps, float guidanceScale)
    {
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
        return memoryStream.ToArray();
    } 
}