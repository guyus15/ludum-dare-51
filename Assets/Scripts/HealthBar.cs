using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image _healthBarInner;

    private void Awake()
    {
        EventManager.AddListener<PlayerSpawnEvent>(OnPlayerSpawn);
        EventManager.AddListener<PlayerHitEvent>(OnPlayerHit);
        EventManager.AddListener<PlayerGainHealthEvent>(OnPlayerGainHealth);
    }

    private void OnPlayerSpawn(PlayerSpawnEvent evt)
    {
        if (_healthBarInner == null) return;

        _healthBarInner.fillAmount = (1.0f / 100.0f) * evt.maxHealth;
    }

    private void OnPlayerHit(PlayerHitEvent evt)
    {
        UpdateBar(evt.currentHealth);
    }

    private void OnPlayerGainHealth(PlayerGainHealthEvent evt)
    {
        UpdateBar(evt.currentHealth);
    }

    private void UpdateBar(int currentHealth)
    {
        if (_healthBarInner == null) return;

        _healthBarInner.fillAmount = (1.0f / 100.0f) * currentHealth;
    }
}
