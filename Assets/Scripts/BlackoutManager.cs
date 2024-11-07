using System.Collections.Generic;
using UnityEngine;

public class BlackoutManager : MonoBehaviour
{
    
    [SerializeField] private List<GameObject> disableOnBlackout;
    [SerializeField] private List<GameObject> enableOnBlackout;

    [SerializeField] private Color ambiantColor;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Blackout();
    }

    // Update is called once per frame
    void Update()
    {
        
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
    }
}
