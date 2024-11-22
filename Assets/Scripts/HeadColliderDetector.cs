using UnityEngine;
using UnityEngine.Events;
public class HeadColliderDetector : MonoBehaviour
{
    [SerializeField] private Transform objectToDetect;
    [SerializeField] private UnityEvent customEvent;

    private void Start()
    {
        if (customEvent == null)
            customEvent = new UnityEvent();
    }

    private void OnTriggerEnter(Collider other)
    {
        print("trigger enter");
        if (other.transform == objectToDetect)
        {
            HeadColliderTriggerModifier headColliderTriggerModifier = null;
            other.TryGetComponent(out headColliderTriggerModifier);
            if (headColliderTriggerModifier != null)
            {
                if (!headColliderTriggerModifier.isCollisionEnabled)
                    return;
            }
            customEvent.Invoke();
        }
    }
}