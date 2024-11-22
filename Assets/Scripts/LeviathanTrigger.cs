using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LeviathanTrigger : MonoBehaviour
{

    private static readonly int AttackTrigger = Animator.StringToHash("Attack");
    [SerializeField] private float delayBeforeSceneChange = 3;
    [SerializeField]
    private Animator leviathanAnimator;

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