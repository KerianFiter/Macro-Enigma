using UnityEngine;

public class CollisionSound : MonoBehaviour
{
    [SerializeField] private string soundToPlay;    // Le nom du son à jouer
    private float _lastPlayTime = -1f;              // Temps du dernier son joué
    [SerializeField] private float cooldown = 0.5f; // Temps minimum entre les sons
    private float _startAfterDelay = 3.0f; // Evite de jouer les sons au démarrage du jeu
    private float _startTime;                       // Temps de démarrage du script
    private bool _canPlaySound = false;             // Contrôle si le son peut être joué

    void Start()
    {
        _startTime = Time.time; // Enregistrer le temps de démarrage
    }

    void Update()
    {
        // Vérifier si 3 secondes se sont écoulées depuis le Start
        if (Time.time - _startTime >= _startAfterDelay)
        {
            _canPlaySound = true; // Permettre de jouer les sons après 3 secondes
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Vérifier si les 3 secondes sont écoulées et que le cooldown est respecté
        if (_canPlaySound)
        {
            float currentTime = Time.time;

            if (currentTime - _lastPlayTime >= cooldown)
            {
                AudioManager.Instance.PlayAudio(soundToPlay);
                _lastPlayTime = currentTime; // Mettre à jour le temps du dernier son joué
            }
        }
    }
}