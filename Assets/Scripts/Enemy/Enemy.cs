﻿using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable, IMoveable
{
    [field: SerializeField, Header("Movement")]
    public float MoveSpeed { get; set; }

    [SerializeField] private GameObject _target;

    [field: SerializeField, Header("Health")]
    public int MaxHealth { get; set; }
    public int CurrentHealth { get; private set; }

    private EnemyManager _enemyManager;
    private Rigidbody2D _rb2d;

    private void Start()
    {
        CurrentHealth = MaxHealth;

        _enemyManager = FindObjectOfType<EnemyManager>();
        _rb2d = GetComponent<Rigidbody2D>();

        _enemyManager.RegisterEnemy(this);
    }

    private void FixedUpdate()
    {
        Move();
    }
    public void Move()
    {
        Vector3 targetDir = _target.transform.position - transform.position;
        targetDir.z = 0;
        float angle = Mathf.Atan2(targetDir.x, targetDir.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, -transform.forward);

        _rb2d.velocity = transform.up * MoveSpeed;
    }

    public void RemoveHealth(int amount)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth - amount, 0, MaxHealth);

        if (CurrentHealth <= 0) Die();
    }

    public void AddHealth(int amount)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth + amount, 0, MaxHealth);
    }

    public void Die()
    {
        _enemyManager.DeregisterEnemy(this);

        Destroy(gameObject);
    }
}
