using System;
using System.Collections.Generic;

namespace KeepItStealthy.Core
{
    public static class EventsManager
    {
        static readonly Dictionary<Type, Action<ApplicationEvent>> events = new Dictionary<Type, Action<ApplicationEvent>>();

        static readonly Dictionary<Delegate, Action<ApplicationEvent>> eventLookups =
            new Dictionary<Delegate, Action<ApplicationEvent>>();

        public static void AddListener<T>(Action<T> evt) where T : ApplicationEvent
        {
            if (!eventLookups.ContainsKey(evt))
            {
                Action<ApplicationEvent> newAction = (e) => evt((T)e);
                eventLookups[evt] = newAction;

                if (events.TryGetValue(typeof(T), out Action<ApplicationEvent> internalAction))
                {
                    events[typeof(T)] = internalAction += newAction;
                }
                else
                {
                    events[typeof(T)] = newAction;
                }
            }
        }

        public static void RemoveListener<T>(Action<T> evt) where T : ApplicationEvent
        {
            if (eventLookups.TryGetValue(evt, out var action))
            {
                if (events.TryGetValue(typeof(T), out var tempAction))
                {
                    tempAction -= action;
                    if (tempAction == null)
                    {
                        events.Remove(typeof(T));
                    }
                    else
                    {
                        events[typeof(T)] = tempAction;
                    }
                }

                eventLookups.Remove(evt);
            }
        }

        public static void Broadcast(ApplicationEvent evt)
        {
            if (events.TryGetValue(evt.GetType(), out var action))
            {
                action.Invoke(evt);
            }
        }

        public static void Clear()
        {
            events.Clear();
            eventLookups.Clear();
        }
    }
}