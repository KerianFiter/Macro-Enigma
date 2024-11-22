using UnityEngine;
public class RespawnIfUnderWorld : MonoBehaviour
{
    private bool _isRbKinematic;

    private Rigidbody _rb;
    private Vector3 _respawnPosition;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _isRbKinematic = _rb.isKinematic;
        _respawnPosition = transform.position;
    }

    private void Update()
    {
        if (transform.position.y < -10)
        {
            _rb.isKinematic = true;
            transform.position = _respawnPosition;
            _rb.isKinematic = _isRbKinematic;
        }
    }
}