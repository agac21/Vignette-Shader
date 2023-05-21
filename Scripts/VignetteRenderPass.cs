using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UIElements;

internal class VignetteRenderPass : ScriptableRenderPass
{
    private readonly Material _vignetteMaterial;
    private RenderTargetIdentifier source { get; set; }
    private RenderTargetHandle destination { get; set; }

    public VignetteRenderPass(Material vignetteMaterial)
    {
        _vignetteMaterial = vignetteMaterial;
        renderPassEvent = RenderPassEvent.AfterRenderingTransparents;
    }

    public void Setup(RenderTargetIdentifier src, RenderTargetHandle dest)
    {
        source = src;
        destination = dest;
    }

    public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
    {
        if (_vignetteMaterial == null)
        {
            return;
        }

        if (!renderingData.cameraData.camera.CompareTag("MainCamera")) return;
        var cmd = CommandBufferPool.Get("VignettePass");
        Blit(cmd, source, destination.Identifier(), _vignetteMaterial);
        context.ExecuteCommandBuffer(cmd);
        CommandBufferPool.Release(cmd);
    }
}