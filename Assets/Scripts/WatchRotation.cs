using System;
using UnityEngine;

public class WatchRotation : MonoBehaviour
{
    [SerializeField] private GameObject cadran;
    [SerializeField] private Transform handMesh;
    [SerializeField] private float initialRotation = -120;

    void Update()
    {
        cadran.transform.localRotation = Quaternion.Euler(-90, 0,  initialRotation - handMesh.eulerAngles.y);
    }
    
}
