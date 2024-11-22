using UnityEngine;
public class HeadPosition : MonoBehaviour
{
    [SerializeField] private Transform centerEye;


    // Update is called once per frame
    private void Update()
    {
        transform.position = centerEye.position;
    }
}