using System;
using System.Collections.Generic;

public abstract class GameEvent { }

public static class EventManager
{
    private static readonly Dictionary<Type, Action<GameEvent>> s_Events = new Dictionary<Type, Action<GameEvent>>();
    private static readonly Dictionary<Delegate, Action<GameEvent>> s_EventLookups = new Dictionary<Delegate, Action<GameEvent>>();

    public static void AddListener<T>(Action<T> evt) where T : GameEvent
    {
        if (!s_EventLookups.ContainsKey(evt))
        {
            // Casting new game event action to be of type T
            void NewAction(GameEvent e) => evt((T)e);
            s_EventLookups[evt] = NewAction;

            // If we already have an event type T in the dictionary, add new action onto existing action.
            if (s_Events.TryGetValue(typeof(T), out Action<GameEvent> internalAction))
                s_Events[typeof(T)] = internalAction += NewAction;
            else
                s_Events[typeof(T)] = NewAction;
        }
    }

    public static void RemoveListener<T>(Action<T> evt) where T : GameEvent
    {
        if (s_EventLookups.TryGetValue(evt, out Action<GameEvent> action))
        {
            if (s_Events.TryGetValue(typeof(T), out Action<GameEvent> tempAction))
            {
                tempAction -= action;
                if (tempAction == null)
                    s_Events.Remove(typeof(T));
                else
                    s_Events[typeof(T)] = tempAction;
            }

            s_EventLookups.Remove(evt);
        }
    }

    public static void Broadcast(GameEvent evt)
    {
        if (s_Events.TryGetValue(evt.GetType(), out Action<GameEvent> action))
            action.Invoke(evt);
    }

    public static void ClearListeners()
    {
        s_Events.Clear();
        s_EventLookups.Clear();
    }
}