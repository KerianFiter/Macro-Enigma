using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class LeviathanTrigger : MonoBehaviour
{
    [SerializeField] private float delayBeforeSceneChange = 3;
    [SerializeField] Animator leviathanAnimator;

    private static readonly int AttackTrigger = Animator.StringToHash("Attack");

    public void TriggerLeviathan()
    {
        StartCoroutine(TriggerLeviathanCoroutine());
    }

    public IEnumerator TriggerLeviathanCoroutine()
    {
        AudioManager.Instance.PlayAudio("scream");
        leviathanAnimator.SetTrigger(AttackTrigger);
        yield return new WaitForSeconds(delayBeforeSceneChange);
        SceneManager.LoadScene("Main");
    }
}
