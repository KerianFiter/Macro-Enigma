using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class ControllerScript : MonoBehaviour
{
    public float speed = 5.0f;
    private Rigidbody rb;
    public CapsuleCollider headCollider;
    public Transform headTransform;
    public float footStepMinDelay = 1f;
    public float footStepMaxDelay = 1.5f;
    private bool isMoving = false;
    private Coroutine footstepCoroutine;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 thumbstick = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
    
        // Set collider trigger based on thumbstick movement
        headCollider.isTrigger = thumbstick == Vector2.zero;

        // Calculate movement based on head forward direction
        float step = Time.fixedTime * speed;
        Vector3 forward = headTransform.forward * thumbstick.y * step;
        Vector3 right = headTransform.right * thumbstick.x * step;
        Vector3 movement = forward + right;

        rb.AddForce(movement, ForceMode.Impulse);

        // Check if player is moving
        isMoving = movement.magnitude > 0.01f;

        // Start or stop the footstep sound coroutine based on movement
        if (isMoving && footstepCoroutine == null)
        {
            footstepCoroutine = StartCoroutine(PlayFootstepSound());
        }
        else if (!isMoving && footstepCoroutine != null)
        {
            StopCoroutine(footstepCoroutine);
            footstepCoroutine = null;
        }

        // Rotate the player based on button presses
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
    }

    private IEnumerator PlayFootstepSound()
    {
        while (isMoving)
        {
            AudioManager.Instance.PlayAudio("footSteps");
            
            float randomStepDelay = Random.Range(footStepMinDelay, footStepMaxDelay);
            yield return new WaitForSeconds(randomStepDelay);
        }
    }
}
