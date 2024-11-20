using UnityEngine;

public class AudioColliderTrigger : MonoBehaviour
{
    [SerializeField] private string audioId;
    
    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player") || other.CompareTag("MainCamera")) {
            AudioManager.Instance.PlayAudio(audioId);
        }
    }
}
