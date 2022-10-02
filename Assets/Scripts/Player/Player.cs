using UnityEngine;

public class Player : MonoBehaviour, IDamagable, IMoveable
{
    [field: SerializeField, Header("Movement")]
    public float MoveSpeed { get; set; }

    [field: SerializeField, Header("Health")]
    public int MaxHealth { get; set; }
    public int CurrentHealth { get; private set; }

    [SerializeField] private float _timeBetweenHealthLoss;
    private float _currentTimeBetweenHealthLoss;

    private bool _canBeDamaged;

    [SerializeField] private BoxCollider2D _hitBox;

    private Rigidbody2D _rb2d;

    private void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;

        CurrentHealth = MaxHealth;

        _canBeDamaged = true;

        // Broadcast a player spawn event/
        PlayerSpawnEvent spawnEvt = Events.s_PlayerSpawnEvent;
        spawnEvt.maxHealth = MaxHealth;
        EventManager.Broadcast(spawnEvt);
    }

    private void Update()
    {
        if (_currentTimeBetweenHealthLoss >= _timeBetweenHealthLoss)
        {
            _canBeDamaged = true;
            _currentTimeBetweenHealthLoss = 0;
        }

        _currentTimeBetweenHealthLoss += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {
        _rb2d.velocity = new Vector2(Input.GetAxis("Horizontal") * MoveSpeed, Input.GetAxis("Vertical") * MoveSpeed);

        Vector3 mousePosition = Input.mousePosition;
        Vector3 playerScreenPos = Camera.main.WorldToScreenPoint(transform.position);

        mousePosition.x -= playerScreenPos.x;
        mousePosition.y -= playerScreenPos.y;

        float angle = Mathf.Atan2(mousePosition.y, mousePosition.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    public void Recoil(float amount, Vector2 direction)
    {
        _rb2d.AddForce(direction * amount, ForceMode2D.Impulse);
    }

    public void RemoveHealth(int amount)
    {
        if (!_canBeDamaged) return;

        CurrentHealth = Mathf.Clamp(CurrentHealth - amount, 0, MaxHealth);

        PlayerHitEvent hitEvt = Events.s_PlayerHitEvent;
        hitEvt.currentHealth = CurrentHealth;
        hitEvt.damageInflicted = amount;
        EventManager.Broadcast(hitEvt);

        if (CurrentHealth <= 0) Die();

        _canBeDamaged = false;
    }

    public void AddHealth(int amount)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth + amount, 0, MaxHealth);
    }

    public void Die()
    {
        PlayerDeathEvent deathEvt = Events.s_PlayerDeathEvent;
        EventManager.Broadcast(deathEvt);
    }
}
