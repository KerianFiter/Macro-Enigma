using System;
using Oculus.Interaction.Input;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using Oculus.Interaction.Grab;
using Oculus.Interaction.GrabAPI;
using Unity.VisualScripting;

public class ChainController : MonoBehaviour
{
    SphereCollider sphereCollider;
    public bool isRightHand;
    public GameObject chain;

    public bool isColliding, isRepeatable;
    public HandGrabAPI rightHandGrabAPI, leftHandGrabAPI;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        sphereCollider = GetComponent<SphereCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == chain)
        {
            isColliding = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == chain)
        {
            isColliding = false;
            isRepeatable = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == chain)
        {
            isRepeatable = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
