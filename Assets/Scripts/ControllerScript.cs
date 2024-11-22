using System.Collections;
using UnityEngine;
public class ControllerScript : MonoBehaviour
{

    [SerializeField] private float speed = 5.0f;
    [SerializeField] private CapsuleCollider headCollider;
    [SerializeField] private Transform headTransform;
    [SerializeField] private float footStepMinDelay = 1f;
    [SerializeField] private float footStepMaxDelay = 1.5f;
    private Coroutine _footstepCoroutine;
    private bool _isMoving;
    private Rigidbody _rb;
    private Quaternion _targetRotation;

    // Start is called before the first frame update
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _targetRotation = transform.rotation;
    }



    // Update is called once per frame
    private void Update()
    {
        Vector2 thumbstick = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);

        // Set collider trigger based on thumbstick movement
        headCollider.isTrigger = thumbstick == Vector2.zero;

        // Calculate movement based on head forward direction
        float step = Time.deltaTime * speed;
        Vector3 forward = headTransform.forward * thumbstick.y * step;
        Vector3 right = headTransform.right * thumbstick.x * step;
        Vector3 movement = forward + right;

        _rb.AddForce(movement, ForceMode.VelocityChange);

        // Check if player is moving
        _isMoving = movement.magnitude > 0.01f;

        // Start or stop the footstep sound coroutine based on movement
        if (_isMoving && _footstepCoroutine == null)
        {
            _footstepCoroutine = StartCoroutine(PlayFootstepSound());
        }
        else if (!_isMoving && _footstepCoroutine != null)
        {
            StopCoroutine(_footstepCoroutine);
            _footstepCoroutine = null;
        }

        // Rotate the player based on button presses
        if (OVRInput.GetDown(OVRInput.RawButton.X))
        {
            _targetRotation *= Quaternion.Euler(0, -30, 0);
        }
        if (OVRInput.GetDown(OVRInput.RawButton.Y))
        {
            _targetRotation *= Quaternion.Euler(0, 30, 0);
        }
        if (OVRInput.GetDown(OVRInput.Button.SecondaryThumbstickLeft))
        {
            _targetRotation *= Quaternion.Euler(0, -30, 0);
        }
        if (OVRInput.GetDown(OVRInput.Button.SecondaryThumbstickRight))
        {
            _targetRotation *= Quaternion.Euler(0, 30, 0);
        }

        transform.rotation = _targetRotation;
    }

    private IEnumerator PlayFootstepSound()
    {
        while (_isMoving)
        {
            AudioManager.Instance.PlayAudio("footSteps");

            float randomStepDelay = Random.Range(footStepMinDelay, footStepMaxDelay);
            yield return new WaitForSeconds(randomStepDelay);
        }
    }
}