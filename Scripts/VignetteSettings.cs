using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;


[Serializable]
public class VignetteSettings
{
    public RenderPassEvent Event = RenderPassEvent.AfterRenderingTransparents;
    public Material vignetteMaterial;
    public Color vignetteColor;
    [Range(0, 1)] public float radius;
    [Range(0, 1)] public float intensity;

    public void ApplySettingsToMaterial()
    {
        vignetteMaterial.SetColor("_Color", vignetteColor);
        vignetteMaterial.SetFloat("_Radius", radius);
        vignetteMaterial.SetFloat("_Intensity", intensity);
    }
}