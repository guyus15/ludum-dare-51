using System;
using UnityEngine;

public abstract class Notification : MonoBehaviour
{
    public string title;
    public string description;
    public float delayVisible;

    public static event Action<Notification> OnNotificationCreate;
    public static event Action<Notification> OnNotificationFinished;

    protected virtual void Start()
    {
        OnNotificationCreate?.Invoke(this);

        DisplayMessageEvent displayEvt = Events.s_DisplayMessageEvent;
        displayEvt.message = title;
        displayEvt.delayBeforeDisplay = 0f;
        EventManager.Broadcast(displayEvt);
    }
}