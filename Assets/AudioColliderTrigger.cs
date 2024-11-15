using UnityEngine;

public class AudioColliderTrigger : MonoBehaviour
{
    public AudioClip audioClip;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if(audioClip != null && (other.CompareTag("Player") || other.CompareTag("MainCamera"))) {
            FindFirstObjectByType<AudioManager>().GetComponent<AudioSource>().PlayOneShot(audioClip);
        }
    }
}
