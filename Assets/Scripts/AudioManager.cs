using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Define a struct to store the AudioClip, ID, and delay pair
    [System.Serializable]
    public struct AudioDelayPair
    {
        public string id;
        public AudioClip audioClip; 
        public float delay;  
        public float volume;
    }

    public List<AudioDelayPair> audioDelayPairs = new List<AudioDelayPair>();

    private Dictionary<string, AudioDelayPair> audioDictionary;

    void Awake()
    {
        audioDictionary = new Dictionary<string, AudioDelayPair>();
        foreach (var pair in audioDelayPairs)
        {
            if (!audioDictionary.ContainsKey(pair.id))
            {
                audioDictionary.Add(pair.id, pair);
            }
            else
            {
                Debug.LogWarning("Duplicate ID found: " + pair.id);
            }
        }
    }

    void Start()
    {
        StartCoroutine(PlayAudioWithDelays());
    }

    private IEnumerator PlayAudioWithDelays()
    {
        foreach (var pair in audioDelayPairs)
        {
            if (pair.audioClip != null)
            {
                yield return new WaitForSeconds(pair.delay);
                
                PlayClip(pair.audioClip, pair.volume);
                Debug.Log("Playing " + pair.audioClip.name + " after " + pair.delay + " seconds delay.");
            }
            else
            {
                Debug.LogWarning("AudioClip is not assigned for ID: " + pair.id);
            }
        }
    }

    private void PlayClip(AudioClip clip, float volume = 1.0f)
    {
        AudioSource audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.volume = volume;
        audioSource.clip = clip;
        audioSource.Play();

        Destroy(audioSource, clip.length);
    }
}
