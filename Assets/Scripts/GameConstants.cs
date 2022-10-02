﻿using UnityEngine;

public class GameConstants
{
    // Movement
    public const string HORIZONTAL_AXIS = "Horizontal";
    public const string VERTICAL_AXIS = "Vertical";

    // Buttons
    public const string SHOOT_BTN = "Fire1";
    public const KeyCode RELOAD_BTN = KeyCode.R;
    public const KeyCode PAUSE_BTN = KeyCode.Escape;

    // Enemy
    public const int MAX_ENEMIES = 100;

    // Modifiers
    public const int MAX_PLAYER_DAMAGE_INCREASE = 50;
    public const int MIN_PLAYER_DAMAGE_INCREASE = 5;

    public const int MAX_PLAYER_BULLET_SPREAD_INCREASE = 30;
    public const int MIN_PLAYER_BULLET_SPREAD_INCREASE = 5;

    public const int MAX_PLAYER_FIRE_RATE_INCREASE = 50;
    public const int MIN_PLAYER_FIRE_RATE_INCREASE = 10;
}