using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;

    [SerializeField] private float _timeBetweenSpawns;
    private float _currentTime = 0.0f;

    private EnemyManager _enemyManager;

    private void Start()
    {
        _enemyManager = FindObjectOfType<EnemyManager>();
    }

    private void Update()
    {
        if (_currentTime >= _timeBetweenSpawns && CanSpawn())
        {
            Spawn();

            _currentTime = 0.0f;
        }

        _currentTime += Time.deltaTime;
    }

    private bool CanSpawn()
    {
        return _enemyManager?.CurrentNumberOfEnemies < GameConstants.MAX_ENEMIES;
    }

    private void Spawn()
    {
        Instantiate(_enemyPrefab, transform.position, transform.rotation);
    }
}
