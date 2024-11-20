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

    private bool isSwimming = false;
    private bool chainSoundPlayed = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

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

        // Apply movement force and play "swim" sound if moving
        if (movement.magnitude > 0.01f)
        {
            rb.AddForce(movement, ForceMode.Force);

            if (!isSwimming)
            {
                AudioManager.Instance.PlayAudio("swim");
                isSwimming = true;
            }
        }
        else
        {
            isSwimming = false;
        }

        // Rotate the player based on button presses / right joystick
        if (OVRInput.GetDown(OVRInput.RawButton.X))
        {
            transform.rotation *= Quaternion.Euler(0, -30, 0);
        }
        if (OVRInput.GetDown(OVRInput.RawButton.Y))
        {
            transform.rotation *= Quaternion.Euler(0, 30, 0);
        }
        if (OVRInput.GetDown(OVRInput.Button.SecondaryThumbstickLeft))
        {
            transform.rotation *= Quaternion.Euler(0, -30, 0);
        }
        if (OVRInput.GetDown(OVRInput.Button.SecondaryThumbstickRight))
        {
            transform.rotation *= Quaternion.Euler(0, 30, 0);
        }

        // UNDERWATER logic with chain grabbing
        bool hasCollided = false;
        for (int i = 0; i < chainControllers.Count; i++)
        {
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
            chainSoundPlayed = false;
            return;
        }

        // Check for left or right hand grabbing and play "chain" sound once at the start of grab
        if (!chainControllers[previousActiveIndex].isRightHand)
        {
            if (OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger) > 0.8f || chainControllers[previousActiveIndex].leftHandGrabAPI.IsHandPalmGrabbing(GrabbingRule.FullGrab))
            {
                Vector3 leftHandGlobalPosition = PlayerControllerTransform.TransformPoint(OVRInput.GetLocalControllerPosition(OVRInput.Controller.LHand));
                
                if (initialLeftHandPosition == Vector3.zero)
                {
                    initialLeftHandPosition = leftHandGlobalPosition;
                }

                PlayerControllerTransform.transform.position += initialLeftHandPosition - leftHandGlobalPosition;

                if (!chainSoundPlayed)
                {
                    AudioManager.Instance.PlayAudio("chain");
                    chainSoundPlayed = true;
                }
            }
            else
            {
                initialLeftHandPosition = Vector3.zero;
                chainSoundPlayed = false;
            }
        }
        else
        {
            if (OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger) > 0.8f || chainControllers[previousActiveIndex].rightHandGrabAPI.IsHandPalmGrabbing(GrabbingRule.FullGrab))
            {
                Vector3 rightHandGlobalPosition = PlayerControllerTransform.TransformPoint(OVRInput.GetLocalControllerPosition(OVRInput.Controller.RHand));
                
                if (initialRightHandPosition == Vector3.zero)
                {
                    initialRightHandPosition = rightHandGlobalPosition;
                }

                PlayerControllerTransform.transform.position += initialRightHandPosition - rightHandGlobalPosition;

                if (!chainSoundPlayed)
                {
                    AudioManager.Instance.PlayAudio("chain");
                    chainSoundPlayed = true;
                }
            }
            else
            {
                initialRightHandPosition = Vector3.zero;
                chainSoundPlayed = false;
            }
        }
    }
}
