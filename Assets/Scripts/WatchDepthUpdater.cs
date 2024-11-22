using System;
using TMPro;
using UnityEngine;
public class WatchDepthUpdater : MonoBehaviour
{
    [SerializeField] private TextMeshPro depthText;

    private void Update()
    {
        depthText.text = Math.Round(Mathf.Abs(transform.position.y - 587)) + "m";
    }
}