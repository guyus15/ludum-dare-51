using System;
using System.Collections.Generic;
using UnityEngine;

public class Modifiers : MonoBehaviour
{
    public PlayerDamageIncreaseModifier PlayerDamageIncreaseModifier { get; private set; }

    public List<IModifier> AllModifiers { get; private set; }

    private void Start()
    {
        PlayerDamageIncreaseModifier = gameObject.AddComponent<PlayerDamageIncreaseModifier>();

        AllModifiers = new List<IModifier>()
        {
            PlayerDamageIncreaseModifier
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