using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction.GrabAPI;

public class UnderwaterController : MonoBehaviour
{
    public List<ChainController> chainControllers;
    private int previousActiveIndex = 0;
    public Transform PlayerControllerTransform;
    Vector3 initialLeftHandPosition = Vector3.zero;
    Vector3 initialRightHandPosition = Vector3.zero;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bool hasCollided = false;
        for (int i = 0; i < chainControllers.Count; i++)
        {
            // On essaye de répéter un Grab sur la chaîne
            if (chainControllers[i].isColliding)
            {
                hasCollided = true;
                if (i != previousActiveIndex)
                {
                    if (previousActiveIndex != -1)
                    {
                        chainControllers[previousActiveIndex].isColliding = false;
                        initialLeftHandPosition = Vector3.zero;
                        initialRightHandPosition = Vector3.zero;
                    }
                    previousActiveIndex = i;
                    break;
                }
            }
        }
        
        if (!hasCollided)
        {
            previousActiveIndex = -1;
            initialLeftHandPosition = Vector3.zero;
            initialRightHandPosition = Vector3.zero;
            return;
        }
        
        if (!chainControllers[previousActiveIndex].isRightHand)
        {
            if(OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger) > 0.8f || chainControllers[previousActiveIndex].leftHandGrabAPI.IsHandPalmGrabbing(GrabbingRule.FullGrab))
            {
                Vector3 leftHandGlobalPosition = PlayerControllerTransform.TransformPoint(OVRInput.GetLocalControllerPosition(OVRInput.Controller.LHand));
                
                if (initialLeftHandPosition == Vector3.zero)
                {
                    initialLeftHandPosition = leftHandGlobalPosition;
                }
                
                PlayerControllerTransform.transform.position += initialLeftHandPosition - leftHandGlobalPosition;
            }
            else
            {
                initialLeftHandPosition = Vector3.zero;
            }
        }
        else
        {
            if(OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger) > 0.8f || chainControllers[previousActiveIndex].rightHandGrabAPI.IsHandPalmGrabbing(GrabbingRule.FullGrab))
            {
                Vector3 rightHandGlobalPosition = PlayerControllerTransform.TransformPoint(OVRInput.GetLocalControllerPosition(OVRInput.Controller.RHand));
                
                if (initialRightHandPosition == Vector3.zero)
                {
                    initialRightHandPosition = rightHandGlobalPosition;
                }
                
                PlayerControllerTransform.transform.position += initialRightHandPosition - rightHandGlobalPosition;
            }
            else
            {
                initialRightHandPosition = Vector3.zero;
            }
        }
    }
}
