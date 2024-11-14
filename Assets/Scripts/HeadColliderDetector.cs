using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

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
        if (other.transform == objectToDetect)
        {
            HeadColliderTriggerModifier headColliderTriggerModifier = null;
            other.TryGetComponent<HeadColliderTriggerModifier>(out headColliderTriggerModifier );
            if (headColliderTriggerModifier != null)
            {
                if (!headColliderTriggerModifier.isCollisionEnabled)
                    return;
            }
            customEvent.Invoke();
        }
    }
}
