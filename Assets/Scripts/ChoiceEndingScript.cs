using System.Collections;
using UnityEngine;
public class ChoiceEndingScript : MonoBehaviour
{

    [SerializeField] private GameObject reality, dream;
    [SerializeField] private GameObject doorClosed;
    [SerializeField] private GameObject doorOpened;
    [SerializeField] private GameObject pills, mask, beep;
    [SerializeField] private EndCredits endCredits;

    public void ChooseDream()
    {
        mask.SetActive(false);
        pills.SetActive(false);

        reality.SetActive(false);
        dream.SetActive(true);
        beep.GetComponent<AudioSource>().Stop();
        StartCoroutine(ShowCreditsCoroutine());
    }

    public void ChooseReality()
    {
        pills.SetActive(false);
        mask.SetActive(false);

        dream.SetActive(false);
        reality.SetActive(true);

        doorClosed.SetActive(false);
        doorOpened.SetActive(true);
        StartCoroutine(ShowCreditsCoroutine());
    }

    private IEnumerator ShowCreditsCoroutine()
    {
        yield return new WaitForSeconds(5);
        endCredits.StartTextSequence();
    }
}