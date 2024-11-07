using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class SceneInitializer : MonoBehaviour
{
    [SerializeField] private VolumeProfile volumeProfile;

    void Start()
    {
        var urpAsset = (UniversalRenderPipelineAsset)GraphicsSettings.currentRenderPipeline;
        urpAsset.volumeProfile = volumeProfile;
    }
}
