using System.Collections;
using UnityEngine;

public class ChoiceEndingScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public GameObject reality, dream, door_closed, door_opened, pills, mask;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChooseDream()
    {
        mask.SetActive(false);
        pills.SetActive(false);

        // TODO: Fondu au noir

        reality.SetActive(false);
        dream.SetActive(true);
    }

    public void ChooseReality()
    {
        pills.SetActive(false);
        mask.SetActive(false);

        dream.SetActive(false);
        reality.SetActive(true);

        // TODO: Fondu au blanc

        door_closed.SetActive(false);
        door_opened.SetActive(true);
    }

    IEnumerator showCredits()
    {
        yield return new WaitForSeconds(5);


    }
}
