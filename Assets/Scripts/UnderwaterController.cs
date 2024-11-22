using System.Collections.Generic;
using Oculus.Interaction.GrabAPI;
using UnityEngine;
using UnityEngine.Serialization;
public class UnderwaterController : MonoBehaviour
{
    [SerializeField] private List<ChainController> chainControllers;
    [FormerlySerializedAs("PlayerControllerTransform")]
    [SerializeField] private Transform playerControllerTransform;

    [SerializeField] private float speed = 5.0f;
    [SerializeField] private CapsuleCollider headCollider;
    [SerializeField] private Transform headTransform;
    private bool _chainSoundPlayed;
    private Vector3 _initialLeftHandPosition = Vector3.zero;
    private Vector3 _initialRightHandPosition = Vector3.zero;

    private bool _isSwimming;
    private int _previousActiveIndex;
    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Vector2 thumbstick = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);

        headCollider.isTrigger = thumbstick == Vector2.zero;

        float step = Time.deltaTime * speed;
        Vector3 forward = headTransform.forward * thumbstick.y * step;
        Vector3 right = headTransform.right * thumbstick.x * step;
        Vector3 movement = forward + right;

        if (movement.magnitude > 0.01f)
        {
            _rb.AddForce(movement, ForceMode.Force);

            if (!_isSwimming)
            {
                AudioManager.Instance.PlayAudio("swim");
                _isSwimming = true;
            }
        }
        else
        {
            _isSwimming = false;
        }

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

        bool hasCollided = false;
        for (int i = 0; i < chainControllers.Count; i++)
        {
            if (chainControllers[i].isColliding)
            {
                hasCollided = true;
                if (i != _previousActiveIndex)
                {
                    if (_previousActiveIndex != -1)
                    {
                        chainControllers[_previousActiveIndex].isColliding = false;
                        _initialLeftHandPosition = Vector3.zero;
                        _initialRightHandPosition = Vector3.zero;
                    }
                    _previousActiveIndex = i;
                    break;
                }
            }
        }

        if (!hasCollided)
        {
            _previousActiveIndex = -1;
            _initialLeftHandPosition = Vector3.zero;
            _initialRightHandPosition = Vector3.zero;
            _chainSoundPlayed = false;
            return;
        }

        if (!chainControllers[_previousActiveIndex].isRightHand)
        {
            if (OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger) > 0.8f || chainControllers[_previousActiveIndex].leftHandGrabAPI.IsHandPalmGrabbing(GrabbingRule.FullGrab))
            {
                Vector3 leftHandGlobalPosition = playerControllerTransform.TransformPoint(OVRInput.GetLocalControllerPosition(OVRInput.Controller.LHand));

                if (_initialLeftHandPosition == Vector3.zero)
                {
                    _initialLeftHandPosition = leftHandGlobalPosition;
                }

                playerControllerTransform.transform.position += _initialLeftHandPosition - leftHandGlobalPosition;

                if (!_chainSoundPlayed)
                {
                    AudioManager.Instance.PlayAudio("chain");
                    _chainSoundPlayed = true;
                }
            }
            else
            {
                _initialLeftHandPosition = Vector3.zero;
                _chainSoundPlayed = false;
            }
        }
        else
        {
            if (OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger) > 0.8f || chainControllers[_previousActiveIndex].rightHandGrabAPI.IsHandPalmGrabbing(GrabbingRule.FullGrab))
            {
                Vector3 rightHandGlobalPosition = playerControllerTransform.TransformPoint(OVRInput.GetLocalControllerPosition(OVRInput.Controller.RHand));

                if (_initialRightHandPosition == Vector3.zero)
                {
                    _initialRightHandPosition = rightHandGlobalPosition;
                }

                if (Vector3.Distance(playerControllerTransform.transform.position, playerControllerTransform.transform.position + _initialRightHandPosition - rightHandGlobalPosition) < 0.1)
                {
                    playerControllerTransform.transform.position += _initialRightHandPosition - rightHandGlobalPosition;
                }

                if (!_chainSoundPlayed)
                {
                    AudioManager.Instance.PlayAudio("chain");
                    _chainSoundPlayed = true;
                }
            }
            else
            {
                _initialRightHandPosition = Vector3.zero;
                _chainSoundPlayed = false;
            }
        }
    }
}