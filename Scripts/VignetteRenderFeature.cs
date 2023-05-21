using UnityEngine;
using UnityEngine.Rendering.Universal;


public class VignetteRenderFeature : ScriptableRendererFeature
{
    [SerializeField] private VignetteSettings settings;
    private VignetteRenderPass _vignetteRenderPass;


    public override void Create()
    {
        _vignetteRenderPass = new VignetteRenderPass(settings.VignetteMaterial);
        _vignetteRenderPass.renderPassEvent = settings.Event;
        settings.ApplySettingsToMaterial();
    }

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        var src = renderer.cameraColorTarget;
        var dest = RenderTargetHandle.CameraTarget;
        _vignetteRenderPass.Setup(src, dest);
        renderer.EnqueuePass(_vignetteRenderPass);
    }
}