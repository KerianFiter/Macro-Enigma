using UnityEngine;

public class FlickeringLight : MonoBehaviour
{
    private Light lightSource;                          // La lampe que tu veux faire grésiller
    [SerializeField] private float minIntensity = 0.5f; // Intensité minimale du grésillement
    [SerializeField] private float maxIntensity = 3.0f; // Intensité maximale du grésillement
    [SerializeField] private float flickerSpeed = 0.1f; // Vitesse du grésillement (plus c'est petit, plus c'est rapide)

    private float targetIntensity;    // Intensité vers laquelle on va grésiller
    private float flickerTimer;

    void Start()
    {
        if (lightSource == null)
        {
            lightSource = GetComponent<Light>();
        }
        SetRandomIntensity();
    }

    void Update()
    {
        // Lisse la transition vers la nouvelle intensité cible
        lightSource.intensity = Mathf.Lerp(lightSource.intensity, targetIntensity, flickerSpeed);

        // Si on est assez proche de l'intensité cible, on en sélectionne une nouvelle
        flickerTimer -= Time.deltaTime;
        if (flickerTimer <= 0f)
        {
            SetRandomIntensity();
        }
    }

    void SetRandomIntensity()
    {
        targetIntensity = Random.Range(minIntensity, maxIntensity);
        flickerTimer = Random.Range(0.05f, 0.2f); // Le temps avant de changer d'intensité
    }
}