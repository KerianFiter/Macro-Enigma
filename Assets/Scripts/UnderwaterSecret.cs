using UnityEngine;

public class UnderwaterSecret : MonoBehaviour
{
    public void UnlockSecret()
    {
        SaveManager.Instance.UnlockUnderwaterSecret();
    }
}
