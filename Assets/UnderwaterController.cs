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
    
    public float speed = 5.0f;
    private Rigidbody rb;
    public CapsuleCollider headCollider;
    public Transform headTransform;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // NAGE
        Vector2 thumbstick = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
    
        // Set collider trigger based on thumbstick movement
        headCollider.isTrigger = thumbstick == Vector2.zero;

        // Calculate movement based on head forward direction
        float step = Time.deltaTime * speed;
        Vector3 forward = headTransform.forward * thumbstick.y * step;
        Vector3 right = headTransform.right * thumbstick.x * step;
        Vector3 movement = forward + right;

        rb.AddForce(movement, ForceMode.Force);

        // Rotate the player based on button presses
        if (OVRInput.GetDown(OVRInput.RawButton.X))
        {
            transform.rotation *= Quaternion.Euler(0, -30, 0);
        }

        if (OVRInput.GetDown(OVRInput.RawButton.Y))
        {
            transform.rotation *= Quaternion.Euler(0, 30, 0);
        }
        
        // UNDERWATER
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
