using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;
    [SerializeField] private GameObject underwaterSecret;
    [SerializeField] private List<GameObject> dontDestroyOnLoadObjects;
    private bool _firstLoad;

    private void Awake()
    {
        if (Instance == null)
        {
            print("First load");
            _firstLoad = true;
            Instance = this;
            PlayerPrefs.DeleteAll();
            DontDestroyOnLoad(this);
        }
        else
        {
            Instance._firstLoad = false;
            Destroy(gameObject);
        }
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Start()
    {
        if (_firstLoad)
        {
            if (BlackoutManager.Instance.enabled)
                BlackoutManager.Instance.Blackout();
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Main")
        {
            if (BlackoutManager.Instance.enabled)
                BlackoutManager.Instance.LightsOn();
            foreach (GameObject obj in dontDestroyOnLoadObjects)
            {
                foreach (MeshRenderer meshRenderer in obj.GetComponentsInChildren<MeshRenderer>())
                {
                    meshRenderer.enabled = true;
                }
            }
        }
        else
        {
            foreach (GameObject obj in dontDestroyOnLoadObjects)
            {
                foreach (MeshRenderer meshRenderer in obj.GetComponentsInChildren<MeshRenderer>())
                {
                    meshRenderer.enabled = false;
                }
            }
        }
    }

    public void UnlockUnderwaterSecret()
    {
        underwaterSecret.SetActive(true);
    }
}