using System;
using System.Collections.Generic;
using Oculus.Interaction;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;
    bool firstLoad;
    [SerializeField] private GameObject underwaterSecret;
    [SerializeField] private List<GameObject> dontDestroyOnLoadObjects;
    
    private void Awake()
    {
        if (Instance == null)
        {
            print("First load");
            firstLoad = true;
            Instance = this;
            PlayerPrefs.DeleteAll();
            DontDestroyOnLoad(this);
        }
        else
        {
            Instance.firstLoad = false;
            Destroy(gameObject);
        }
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Start()
    {
        if (firstLoad)
        {
            if(BlackoutManager.Instance.enabled)
                BlackoutManager.Instance.Blackout();
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.name == "Main")
        {
            if(BlackoutManager.Instance.enabled)
                BlackoutManager.Instance.LightsOn();
            foreach (var obj in dontDestroyOnLoadObjects)
            {
                foreach (var meshRenderer in obj.GetComponentsInChildren<MeshRenderer>())
                {
                    meshRenderer.enabled = true;
                }
            }
        }
        else
        {
            foreach (var obj in dontDestroyOnLoadObjects)
            {
                foreach (var meshRenderer in obj.GetComponentsInChildren<MeshRenderer>())
                {
                    meshRenderer.enabled = false;
                }
            }
        }
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void UnlockUnderwaterSecret()
    {
        underwaterSecret.SetActive(true);
    }
}
