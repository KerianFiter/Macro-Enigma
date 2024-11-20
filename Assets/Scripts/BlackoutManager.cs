using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

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
        RenderSettings.defaultReflectionMode = UnityEngine.Rendering.DefaultReflectionMode.Custom;
        foreach (var item in disableOnBlackout)
        {
            item.SetActive(false);
        }
        foreach (var item in disableOnBlackoutRenderers)
        {
            item.enabled = false;
        }
        foreach (var item in enableOnBlackout)
        {
            item.SetActive(true);
        }
    }
    
    public void LightsOn()
    {
        RenderSettings.fog = true;
        RenderSettings.ambientLight = ambiantColor;
        RenderSettings.defaultReflectionMode = UnityEngine.Rendering.DefaultReflectionMode.Skybox;
        foreach (var item in disableOnBlackout)
        {
            item.SetActive(true);
        }
        foreach (var item in enableOnBlackout)
        {
            item.SetActive(false);
        }
        foreach (var item in enableOnLightsOn)
        {
            item.SetActive(true);
        }
        foreach (var item in disableOnLightsOn)
        {
            item.SetActive(false);
        }
        foreach (var item in disableOnBlackoutRenderers)
        {
            item.enabled = true;
        }
    }
}
