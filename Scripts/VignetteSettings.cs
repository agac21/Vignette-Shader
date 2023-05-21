using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Serialization;


[Serializable]
public class VignetteSettings
{
    public RenderPassEvent Event = RenderPassEvent.AfterRendering;
    [SerializeField] private Material m_vignetteMaterial;
    [SerializeField] private Color m_vignetteColor;
    [Range(0, 1)] public float radius;
    [Range(0, 1)] public float intensity;

    public Material VignetteMaterial
    {
        get
        {
            if (m_vignetteMaterial == null)
            {
                Debug.LogError("You should assign the VignetteMaterial to the VignetteRenderFeature on the Universal Renderer Data that you currently used.");
            }

            return m_vignetteMaterial;
        }
    }

    public void ApplySettingsToMaterial()
    {
        if (VignetteMaterial == null)
        {
            return;
        }

        VignetteMaterial.SetColor("_Color", m_vignetteColor);
        VignetteMaterial.SetFloat("_Radius", radius);
        VignetteMaterial.SetFloat("_Intensity", intensity);
    }
}