using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ControllerScript : MonoBehaviour
{
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
    }

    
}
