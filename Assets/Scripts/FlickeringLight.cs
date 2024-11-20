using UnityEngine;

public class FlickeringLight : MonoBehaviour
{
    private Light _lightSource;                          
    [SerializeField] private float minIntensity = 0.5f; // Intensité minimale du grésillement
    [SerializeField] private float maxIntensity = 3.0f; // Intensité maximale du grésillement
    [SerializeField] private float flickerSpeed = 0.1f; // Vitesse du grésillement (plus c'est petit, plus c'est rapide)

    private float _targetIntensity;    // Intensité vers laquelle on va grésiller
    private float _flickerTimer;

    void Start()
    {
        if (_lightSource == null)
        {
            _lightSource = GetComponent<Light>();
        }
        SetRandomIntensity();
    }

    void Update()
    {
        // Lisse la transition vers la nouvelle intensité cible
        _lightSource.intensity = Mathf.Lerp(_lightSource.intensity, _targetIntensity, flickerSpeed);

        // Si on est assez proche de l'intensité cible, on en sélectionne une nouvelle
        _flickerTimer -= Time.deltaTime;
        if (_flickerTimer <= 0f)
        {
            SetRandomIntensity();
        }
    }

    void SetRandomIntensity()
    {
        _targetIntensity = Random.Range(minIntensity, maxIntensity);
        _flickerTimer = Random.Range(0.05f, 0.2f); // Le temps avant de changer d'intensité
    }
}