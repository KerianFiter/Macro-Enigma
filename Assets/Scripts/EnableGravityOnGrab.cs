using System;
using System.Collections.Generic;
using Oculus.Interaction;
using Oculus.Interaction.GrabAPI;
using Oculus.Interaction.HandGrab;
using UnityEngine;

public class EnableGravityOnGrab : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private bool _isGrabbed;
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    
    public void OnGrab()
    {
        _rigidbody.isKinematic = false;
    }
}
