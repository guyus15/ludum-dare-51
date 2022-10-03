using System;
using System.Collections.Generic;
using UnityEngine;

public class Modifiers : MonoBehaviour
{
    public PlayerDamageIncreaseModifier PlayerDamageIncreaseModifier { get; private set; }
    public PlayerBulletSpreadIncreaseModifier PlayerBulletSpreadIncreaseModifier { get; set; }
    public PlayerFireRateIncreaseModifier PlayerFireRateIncreaseModifier { get; set; }
    public PlayerReloadSpeedDecreaseModifier PlayerReloadSpeedDecreaseModifier { get; set; }
    public PlayerReloadSpeedIncreaseModifier PlayerReloadSpeedIncreaseModifier { get; set; }

    public List<IModifier> AllModifiers { get; private set; }

    private void Start()
    {
        PlayerDamageIncreaseModifier = gameObject.AddComponent<PlayerDamageIncreaseModifier>();
        PlayerBulletSpreadIncreaseModifier = gameObject.AddComponent<PlayerBulletSpreadIncreaseModifier>();
        PlayerFireRateIncreaseModifier = gameObject.AddComponent<PlayerFireRateIncreaseModifier>();
        PlayerReloadSpeedDecreaseModifier = gameObject.AddComponent<PlayerReloadSpeedDecreaseModifier>();
        PlayerReloadSpeedIncreaseModifier = gameObject.AddComponent<PlayerReloadSpeedIncreaseModifier>();

        AllModifiers = new List<IModifier>()
        {
            PlayerDamageIncreaseModifier,
            PlayerBulletSpreadIncreaseModifier,
            PlayerFireRateIncreaseModifier,
            PlayerReloadSpeedDecreaseModifier,
            PlayerReloadSpeedIncreaseModifier
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
        Name = "Player Damage Increased";

        _playerWeaponManager = FindObjectOfType<WeaponManager>();
    }

    public void Activate()
    {
        System.Random random = new System.Random();
        DamageIncreaseAmount = random.Next(GameConstants.MIN_PLAYER_DAMAGE_INCREASE, GameConstants.MAX_PLAYER_DAMAGE_INCREASE);

        _playerWeaponManager.ActiveWeapon.IncreaseBulletDamage(DamageIncreaseAmount);

        NotifyPlayerEvent notifyPlayerEvent = Events.s_NotifyPlayerEvent;
        notifyPlayerEvent.titleText = Name;
        notifyPlayerEvent.descriptionText = $"Increased player damage by {DamageIncreaseAmount} points.";
        notifyPlayerEvent.delayVisible = 3.0f;
        EventManager.Broadcast(notifyPlayerEvent);
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
        Name = "Player Bullet Spread Increased";

        _playerWeaponManager = FindObjectOfType<WeaponManager>();
    }

    public void Activate()
    {
        System.Random random = new System.Random();
        SpreadIncreaseAmount = random.Next(GameConstants.MIN_PLAYER_BULLET_SPREAD_INCREASE, GameConstants.MAX_PLAYER_BULLET_SPREAD_INCREASE);

        _playerWeaponManager.ActiveWeapon.IncreaseBulletSpread(SpreadIncreaseAmount);

        NotifyPlayerEvent notifyPlayerEvent = Events.s_NotifyPlayerEvent;
        notifyPlayerEvent.titleText = Name;
        notifyPlayerEvent.descriptionText = $"Increased bullet spread by {SpreadIncreaseAmount} degrees.";
        notifyPlayerEvent.delayVisible = 3.0f;
        EventManager.Broadcast(notifyPlayerEvent);
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
        Name = "Player Fire Rate Increased";

        _playerWeaponManager = FindObjectOfType<WeaponManager>();
    }

    public void Activate()
    {
        System.Random random = new System.Random();
        FireRateIncrease = random.Next(GameConstants.MIN_PLAYER_FIRE_RATE_INCREASE, GameConstants.MAX_PLAYER_FIRE_RATE_INCREASE);

        _playerWeaponManager.ActiveWeapon.IncreaseFireRate(FireRateIncrease);

        NotifyPlayerEvent notifyPlayerEvent = Events.s_NotifyPlayerEvent;
        notifyPlayerEvent.titleText = Name;
        notifyPlayerEvent.descriptionText = $"Increased player fire rate by {FireRateIncrease} bullets per seconds.";
        notifyPlayerEvent.delayVisible = 3.0f;
        EventManager.Broadcast(notifyPlayerEvent);
    }

    public void Deactivate()
    {
        _playerWeaponManager.ActiveWeapon.ResetFireRate();
    }
}

public class PlayerReloadSpeedDecreaseModifier : MonoBehaviour, IModifier
{
    public string Name { get; set; }
    public float ReloadSpeedIncrease { get; private set; }

    private WeaponManager _playerWeaponManager;

    private void Awake()
    {
        Name = "Player Reload Speed Decreased";

        _playerWeaponManager = FindObjectOfType<WeaponManager>();
    }

    public void Activate()
    {
        System.Random random = new System.Random();
        ReloadSpeedIncrease = random.Next(GameConstants.MIN_PLAYER_RELOAD_SPEED_INCREASE, GameConstants.MAX_PLAYER_RELOAD_SPEED_INCREASE);

        _playerWeaponManager.ActiveWeapon.DecreaseReloadSpeed(ReloadSpeedIncrease);

        NotifyPlayerEvent notifyPlayerEvent = Events.s_NotifyPlayerEvent;
        notifyPlayerEvent.titleText = Name;
        notifyPlayerEvent.descriptionText = $"Decreased player reload speed by {1 / ReloadSpeedIncrease:0.#} seconds.";
        notifyPlayerEvent.delayVisible = 3.0f;
        EventManager.Broadcast(notifyPlayerEvent);
    }

    public void Deactivate()
    {
        _playerWeaponManager.ActiveWeapon.ResetReloadTime();
    }
}
public class PlayerReloadSpeedIncreaseModifier : MonoBehaviour, IModifier
{
    public string Name { get; set; }
    public float ReloadSpeedIncrease { get; private set; }

    private WeaponManager _playerWeaponManager;

    private void Awake()
    {
        Name = "Player Reload Speed Increased";

        _playerWeaponManager = FindObjectOfType<WeaponManager>();
    }

    public void Activate()
    {
        System.Random random = new System.Random();
        ReloadSpeedIncrease = random.Next(GameConstants.MIN_PLAYER_RELOAD_SPEED_INCREASE, GameConstants.MAX_PLAYER_RELOAD_SPEED_INCREASE);

        _playerWeaponManager.ActiveWeapon.IncreaseReloadSpeed(ReloadSpeedIncrease);

        NotifyPlayerEvent notifyPlayerEvent = Events.s_NotifyPlayerEvent;
        notifyPlayerEvent.titleText = Name;
        notifyPlayerEvent.descriptionText = $"Increased player reload speed by {1 / ReloadSpeedIncrease:0.#} seconds.";
        notifyPlayerEvent.delayVisible = 3.0f;
        EventManager.Broadcast(notifyPlayerEvent);
    }

    public void Deactivate()
    {
        _playerWeaponManager.ActiveWeapon.ResetReloadTime();
    }
}