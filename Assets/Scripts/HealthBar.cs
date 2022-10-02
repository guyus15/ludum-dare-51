using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image _healthBarInner;

    private void Awake()
    {
        EventManager.AddListener<PlayerSpawnEvent>(OnPlayerSpawn);
        EventManager.AddListener<PlayerHitEvent>(OnPlayerHit);
    }

    private void OnPlayerSpawn(PlayerSpawnEvent evt)
    {
        _healthBarInner.fillAmount = (1.0f / 100.0f) * evt.maxHealth;
    }

    private void OnPlayerHit(PlayerHitEvent evt)
    {
        _healthBarInner.fillAmount = (1.0f / 100.0f) * evt.currentHealth;
    }
}
