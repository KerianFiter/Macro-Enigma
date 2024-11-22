using UnityEngine;
public class EnableGravityOnGrab : MonoBehaviour
{
    private bool _isGrabbed;
    private Rigidbody _rigidbody;
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void OnGrab()
    {
        _rigidbody.isKinematic = false;
    }
}