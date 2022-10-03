using UnityEngine;

public class FinishedParticleDestroyer : MonoBehaviour
{
    private ParticleSystem _thisParticleSystem;

    private void Start()
    {
        _thisParticleSystem = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        if (_thisParticleSystem.isPlaying)
            return;

        Destroy(gameObject);
    }
}