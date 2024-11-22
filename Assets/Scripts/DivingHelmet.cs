using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class DivingHelmet : MonoBehaviour
{
    public void StartPutOnAnimation(Transform target)
    {
        StartCoroutine(StartPutOnAnimationCoroutine(target));
    }

    public IEnumerator StartPutOnAnimationCoroutine(Transform target)
    {
        transform.parent = target;
        LeanTween.move(gameObject, target.position, 3).setEase(LeanTweenType.easeOutQuad);
        LeanTween.rotate(gameObject, target.rotation.eulerAngles, 3).setEase(LeanTweenType.easeOutQuad);
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("Underwater");
    }
}