using UnityEngine;

public class SecretPainting : MonoBehaviour
{
    private static SecretPainting _instance;
    
    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(gameObject);
    }
}
