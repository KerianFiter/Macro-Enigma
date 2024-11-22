using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class UnderwterExit : MonoBehaviour
{
    [SerializeField] private Transform transformTrigger;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform == transformTrigger)
            StartCoroutine(ExitUnderwaterCoroutine());
    }

    private IEnumerator ExitUnderwaterCoroutine()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("Main");
    }
}