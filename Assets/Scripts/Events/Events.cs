public static class Events
{
    public static readonly PlayerSpawnEvent s_PlayerSpawnEvent = new PlayerSpawnEvent();
    public static readonly PlayerReloadEvent s_PlayerReloadEvent = new PlayerReloadEvent();
    public static readonly PlayerHitEvent s_PlayerHitEvent = new PlayerHitEvent();
    public static readonly PlayerDeathEvent s_PlayerDeathEvent = new PlayerDeathEvent();
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

public class PlayerDeathEvent : GameEvent { }