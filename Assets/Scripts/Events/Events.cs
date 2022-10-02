public static class Events
{
    public static readonly PlayerReloadEvent s_PlayerReloadEvent = new PlayerReloadEvent();
}
public class PlayerReloadEvent : GameEvent
{
    public delegate void OnReload();

    public OnReload onReloadInstance;
    public float reloadTime;
}