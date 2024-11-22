using System.Collections;
using TMPro;
using UnityEngine;
public class EndCredits : MonoBehaviour
{
    [SerializeField] private TextMeshPro textMeshPro;
    [SerializeField] private float fadeDuration = 1.0f;
    [SerializeField] private float displayDuration = 2.0f;
    [SerializeField] private string[] messages =
    {
        "Développé par", "Mathys Garoui", "Kérian Fiter", "Antoine Barberin", "MACRO ENIGMA"
    };

    private void Start()
    {
        if (textMeshPro != null)
        {
            textMeshPro.alpha = 0;
            textMeshPro.gameObject.SetActive(false);
        }
    }

    public void StartTextSequence()
    {
        if (textMeshPro != null)
        {
            textMeshPro.gameObject.SetActive(true);
            StartCoroutine(FadeTextSequence());
        }
    }

    private IEnumerator FadeTextSequence()
    {
        for (int i = 0; i < messages.Length; i++)
        {
            bool isLastMessage = i == messages.Length - 1;
            if (isLastMessage)
            {
                textMeshPro.fontSize = 6;
                textMeshPro.fontWeight = FontWeight.Bold;
            }
            textMeshPro.text = messages[i];
            yield return FadeIn();
            if (isLastMessage)
            {
                AudioManager.Instance.PlayAudio("boom");
            }
            if (isLastMessage)
            {
                yield return new WaitForSeconds(3 * displayDuration);

            }
            else
            {
                yield return new WaitForSeconds(displayDuration);
            }
            yield return FadeOut();


        }

        textMeshPro.gameObject.SetActive(false);
    }

    private IEnumerator FadeIn()
    {
        float elapsedTime = 0;
        while (elapsedTime < fadeDuration)
        {
            textMeshPro.alpha = Mathf.Lerp(0, 1, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        textMeshPro.alpha = 1;
    }

    private IEnumerator FadeOut()
    {
        float elapsedTime = 0;
        while (elapsedTime < fadeDuration)
        {
            textMeshPro.alpha = Mathf.Lerp(1, 0, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        textMeshPro.alpha = 0;
    }
}