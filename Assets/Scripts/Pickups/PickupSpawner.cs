using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _healthPickupObject;
    [SerializeField] private GameObject _ammoPickupObject;

    private void Awake()
    {
        EventManager.AddListener<EnemyDeathEvent>(SpawnPickup);
    }

    private void SpawnPickup(EnemyDeathEvent evt)
    {
        Debug.Log("Handling spawning of pickup");

        Vector2 spawnLocation = new Vector2(evt.xPos, evt.yPos);

        int healthPickupValue = Random.Range(1, 101);
        int ammoPickupValue = Random.Range(1, 101);

        if (healthPickupValue <= GameConstants.HEALTH_PICKUP_CHANCE)
        {
            Debug.Log("Spawning health pickup");
            Instantiate(_healthPickupObject, spawnLocation, Quaternion.identity);
        }

        if (ammoPickupValue <= GameConstants.AMMO_PICKUP_CHANCE)
        {
            Debug.Log("Spawning ammo pickup");
            Instantiate(_ammoPickupObject, spawnLocation, Quaternion.identity);
        }
    }
}