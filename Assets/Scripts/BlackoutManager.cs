using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
public class BlackoutManager : MonoBehaviour
{
    public static BlackoutManager Instance;
    [SerializeField] private List<GameObject> disableOnBlackout;
    [SerializeField] private List<MeshRenderer> disableOnBlackoutRenderers;
    [SerializeField] private List<GameObject> enableOnBlackout;
    [SerializeField] private List<GameObject> disableOnLightsOn;
    [SerializeField] private List<GameObject> enableOnLightsOn;
    [SerializeField] private Color ambiantColor;

    private void Awake()
    {
        Instance = this;
    }


    public void Blackout()
    {
        RenderSettings.fog = false;
        RenderSettings.ambientLight = Color.black;
        RenderSettings.defaultReflectionMode = DefaultReflectionMode.Custom;
        foreach (GameObject item in disableOnBlackout)
        {
            item.SetActive(false);
        }
        foreach (MeshRenderer item in disableOnBlackoutRenderers)
        {
            item.enabled = false;
        }
        foreach (GameObject item in enableOnBlackout)
        {
            item.SetActive(true);
        }
    }

    public void LightsOn()
    {
        RenderSettings.fog = true;
        RenderSettings.ambientLight = ambiantColor;
        RenderSettings.defaultReflectionMode = DefaultReflectionMode.Skybox;
        foreach (GameObject item in disableOnBlackout)
        {
            item.SetActive(true);
        }
        foreach (GameObject item in enableOnBlackout)
        {
            item.SetActive(false);
        }
        foreach (GameObject item in enableOnLightsOn)
        {
            item.SetActive(true);
        }
        foreach (GameObject item in disableOnLightsOn)
        {
            item.SetActive(false);
        }
        foreach (MeshRenderer item in disableOnBlackoutRenderers)
        {
            item.enabled = true;
        }
    }
}