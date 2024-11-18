using System;
using UnityEngine;

public class WatchRotation : MonoBehaviour
{
    public Transform worldReference;
    public GameObject cadran;
    public Transform handMesh;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cadran.transform.localRotation = Quaternion.Euler(-90, 0, - 45 - handMesh.eulerAngles.y);
    }
    
}
