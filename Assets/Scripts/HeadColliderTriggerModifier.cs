using UnityEngine;

public class HeadColliderTriggerModifier : MonoBehaviour
{
    public bool isCollisionEnabled = true;
    
    public void EnableCollision()
    {
        isCollisionEnabled = true;
    }
    
    public void DisableCollision()
    {
        isCollisionEnabled = false;
    }
}
