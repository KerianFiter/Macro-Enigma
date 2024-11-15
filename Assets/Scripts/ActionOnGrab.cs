using System;
using System.Collections.Generic;
using Oculus.Interaction;
using Oculus.Interaction.GrabAPI;
using Oculus.Interaction.HandGrab;
using UnityEngine;

public class ActionOnGrab : MonoBehaviour
{
    [SerializeField] private List<GameObject> enableOnGrab;
    private Rigidbody _rigidbody;
    private bool _isGrabbed;
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
    }



    public void OnGrab()
    {
        foreach (var item in enableOnGrab)
        {
            item.SetActive(true);
        }
        _rigidbody.isKinematic = false;
    }
    
    
}
