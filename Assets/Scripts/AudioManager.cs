using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    
    // Define a struct to store the AudioClip, ID, and delay pair
    [System.Serializable]
    public struct AudioDelayPair
    {
        public string id;
        public List<AudioClip> audioClip; 
        public float delay;  
        public float volume;
        public bool spatialize;
        public Transform spatializeTransform;
        public bool playOnlyOnce;
        public bool loop;
    }

    [SerializeField]
    private List<AudioDelayPair> audioDelayPairs = new List<AudioDelayPair>();
    private Dictionary<string, AudioDelayPair> _audioDictionary;
    private AudioSource _backgroundAudioSource;
    
    void Awake()
    {
        Instance = this;
        _audioDictionary = new Dictionary<string, AudioDelayPair>();
        foreach (var pair in audioDelayPairs)
        {
            if (!_audioDictionary.ContainsKey(pair.id))
            {
                _audioDictionary.Add(pair.id, pair);
            }
            else
            {
                Debug.LogWarning("Duplicate ID found: " + pair.id);
            }
        }
    }

    void Start()
    {
        if(SceneManager.GetActiveScene().name == "Main")
        {
            PlayAudio("ou_est_ce_que_jai_aterri");
            PlayAudio("BGNoLights");
            
        }
        else if(SceneManager.GetActiveScene().name == "Underwater")
        {
            PlayAudio("BG");
            PlayAudio("leviathan");
        }

    }

    public void PlayAudio(string id)
    {
        bool background = id.Contains("BG");
        StartCoroutine(PlayAudioCoroutine(id, background));
    }
    private IEnumerator PlayAudioCoroutine(string id, bool background = false)
    {
        if (_audioDictionary.ContainsKey(id))
        {
            AudioDelayPair pair = _audioDictionary[id];
            if(PlayerPrefs.HasKey(id) && pair.playOnlyOnce)
            {
                yield break;
            }
            PlayerPrefs.SetInt(id, 1);
            if (pair.audioClip.Count > 0)
            {
                yield return new WaitForSeconds(pair.delay);
                AudioClip clip = pair.audioClip[Random.Range(0, pair.audioClip.Count)];
                PlayClip(clip, pair.loop, pair.spatialize, pair.spatializeTransform, pair.volume, background);
                Debug.Log("Playing " + clip.name + " after " + pair.delay + " seconds delay.");
            }
            else
            {
                Debug.LogWarning("AudioClip is not assigned for ID: " + pair.id);
            }
        }
    }

    private void PlayClip(AudioClip clip, bool loop, bool spatialize, Transform spatializeTransform, float volume = 1.0f,  bool background = false)
    {
        if(background && _backgroundAudioSource != null)
        {
            Destroy(_backgroundAudioSource);
        }
        if (!spatializeTransform)
        {
            spatializeTransform = transform;
        }
        AudioSource audioSource = spatializeTransform.AddComponent<AudioSource>();
        if (background)
        {
            _backgroundAudioSource = audioSource;
        }
        if (spatialize)
        { 
            audioSource.spatialBlend = 1.0f;
        }
        audioSource.spatialize = spatialize;
        audioSource.volume = volume;
        audioSource.clip = clip;
        audioSource.loop = loop;
        audioSource.Play();
        if (!loop)
        {
            Destroy(audioSource, clip.length);
        }
    }
}
