using System;
using System.Collections.Generic;
using UnityEngine;

public class Modifiers : MonoBehaviour
{
    public PlayerDamageIncreaseModifier PlayerDamageIncreaseModifier { get; private set; }
    public PlayerBulletSpreadIncreaseModifier PlayerBulletSpreadIncreaseModifier { get; set; }
    public PlayerFireRateIncreaseModifier PlayerFireRateIncreaseModifier { get; set; }

    public List<IModifier> AllModifiers { get; private set; }

    private void Start()
    {
        PlayerDamageIncreaseModifier = gameObject.AddComponent<PlayerDamageIncreaseModifier>();
        PlayerBulletSpreadIncreaseModifier = gameObject.AddComponent<PlayerBulletSpreadIncreaseModifier>();
        PlayerFireRateIncreaseModifier = gameObject.AddComponent<PlayerFireRateIncreaseModifier>();

        AllModifiers = new List<IModifier>()
        {
            PlayerDamageIncreaseModifier,
            PlayerBulletSpreadIncreaseModifier,
            PlayerFireRateIncreaseModifier
        };
    }
}

public class PlayerDamageIncreaseModifier : MonoBehaviour, IModifier
{
    public string Name { get; set; }
    public int DamageIncreaseAmount { get; private set; }

    private WeaponManager _playerWeaponManager;

    private void Awake()
    {
        Name = "Player Damage Increase Modifier";

        _playerWeaponManager = FindObjectOfType<WeaponManager>();
    }

    public void Activate()
    {
        System.Random random = new System.Random();
        DamageIncreaseAmount = random.Next(GameConstants.MIN_PLAYER_DAMAGE_INCREASE, GameConstants.MAX_PLAYER_DAMAGE_INCREASE);

        _playerWeaponManager.ActiveWeapon.IncreaseBulletDamage(DamageIncreaseAmount);
    }

    public void Deactivate()
    {
        _playerWeaponManager.ActiveWeapon.ResetBulletDamage();
    }
}

public class PlayerBulletSpreadIncreaseModifier : MonoBehaviour, IModifier
{
    public string Name { get; set; }
    public double SpreadIncreaseAmount { get; private set; }

    private WeaponManager _playerWeaponManager;

    private void Awake()
    {
        Name = "Player Bullet Spread Increase Modifier";

        _playerWeaponManager = FindObjectOfType<WeaponManager>();
    }

    public void Activate()
    {
        System.Random random = new System.Random();
        SpreadIncreaseAmount = random.Next(GameConstants.MIN_PLAYER_BULLET_SPREAD_INCREASE, GameConstants.MAX_PLAYER_BULLET_SPREAD_INCREASE);

        _playerWeaponManager.ActiveWeapon.IncreaseBulletSpread(SpreadIncreaseAmount);
    }

    public void Deactivate()
    {
        _playerWeaponManager.ActiveWeapon.ResetBulletSpread();
    }
}

public class PlayerFireRateIncreaseModifier : MonoBehaviour, IModifier
{
    public string Name { get; set; }
    public float FireRateIncrease { get; private set; }

    private WeaponManager _playerWeaponManager;

    private void Awake()
    {
        Name = "Player Fire Rate Increase Modifier";

        _playerWeaponManager = FindObjectOfType<WeaponManager>();
    }

    public void Activate()
    {
        System.Random random = new System.Random();
        FireRateIncrease = random.Next(GameConstants.MIN_PLAYER_FIRE_RATE_INCREASE, GameConstants.MAX_PLAYER_FIRE_RATE_INCREASE);

        _playerWeaponManager.ActiveWeapon.IncreaseFireRate(FireRateIncrease);
    }

    public void Deactivate()
    {
        _playerWeaponManager.ActiveWeapon.ResetFireRate();
    }
}