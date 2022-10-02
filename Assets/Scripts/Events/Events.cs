public static class Events
{
    public static readonly PlayerSpawnEvent s_PlayerSpawnEvent = new PlayerSpawnEvent();
    public static readonly PlayerReloadEvent s_PlayerReloadEvent = new PlayerReloadEvent();
    public static readonly PlayerHitEvent s_PlayerHitEvent = new PlayerHitEvent();
    public static readonly PlayerGainHealthEvent s_PlayerGainHealthEvent = new PlayerGainHealthEvent();
    public static readonly PlayerDeathEvent s_PlayerDeathEvent = new PlayerDeathEvent();
    public static readonly PlayerPickupEvent s_PlayerPickupEvent = new PlayerPickupEvent();
    public static readonly EnemyDeathEvent s_EnemyDeathEvent = new EnemyDeathEvent();
}

public class PlayerSpawnEvent : GameEvent
{
    public int maxHealth;
}

public class PlayerReloadEvent : GameEvent
{
    public delegate void OnReload();

    public OnReload onReloadInstance;
    public float reloadTime;
}

public class PlayerHitEvent : GameEvent
{
    public int currentHealth;
    public int damageInflicted;
}

public class PlayerGainHealthEvent : GameEvent
{
    public int currentHealth;
}

public class PlayerDeathEvent : GameEvent { }

public class PlayerPickupEvent : GameEvent
{
    public enum PickupType
    {
        HEALTH = 0,
        AMMO
    }

    public PickupType type;
}

public class EnemyDeathEvent : GameEvent
{
    public float xPos, yPos;
}