using UnityEngine;

public class HeadPosition : MonoBehaviour
{
    public Transform centerEye;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = centerEye.position;
    }
}
