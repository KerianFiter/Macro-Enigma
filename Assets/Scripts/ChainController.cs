using Oculus.Interaction.GrabAPI;
using UnityEngine;
public class ChainController : MonoBehaviour
{
    public bool isRightHand;
    [SerializeField] private GameObject chain;

    public bool isColliding, isRepeatable;
    public HandGrabAPI rightHandGrabAPI, leftHandGrabAPI;


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
}