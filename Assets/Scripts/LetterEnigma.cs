using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LetterEnigma : MonoBehaviour
{
    private static LetterEnigma _instance;
    [SerializeField] private List<LetterSlot> letterSlots;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(gameObject);
    }
    public void CheckEnigma()
    {
        if (letterSlots[0].GetLetter() == 'C'
            && letterSlots[1].GetLetter() == 'O'
            && letterSlots[2].GetLetter() == 'M'
            && letterSlots[3].GetLetter() == 'A')
        {
            AudioManager.Instance.PlayAudio("coma");
            StartCoroutine(TeleportToHospital());
        }
    }

    private IEnumerator TeleportToHospital()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("Hospital");
    }
}