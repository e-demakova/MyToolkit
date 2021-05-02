using System;
using System.Collections.Generic;

using UnityEngine;

namespace Deblue.ObservingSystem
{
    [RequireComponent(typeof(Collider2D))]
    public class Trigger : MonoBehaviour
    {
        [SerializeField] private LayerMask  _layers = default;

        private Dictionary<Type, Handler<Trigger_Component_React>> _handlers = new Dictionary<Type, Handler<Trigger_Component_React>>(5);
        private Handler<Trigger_React> _triggerEnter = new Handler<Trigger_React>();

        private void OnTriggerEnter2D(Collider2D other)
        {
            ReactOnTrigger(other, true);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            ReactOnTrigger(other, false);
        }

        private void ReactOnTrigger(Collider2D other, bool isEnter)
        {
            if ((1 << other.gameObject.layer & _layers) != 0)
            {
                foreach (var handler in _handlers)
                {
                    if (other.TryGetComponent(handler.Key, out var component))
                    {
                        handler.Value.Raise(new Trigger_Component_React(other, component, isEnter));
                    }
                }
                _triggerEnter.Raise(new Trigger_React(other, isEnter));
            }
        }

        public void SubscribeOnTrigger(Action<Trigger_React> action)
        {
            _triggerEnter.Subscribe(action);
        }
        
        public void UnsubscribeOnTrigger(Action<Trigger_React> action)
        {
            _triggerEnter.Unsubscribe(action);
        }

        public void SubscribeOnTrigger<T>(Action<Trigger_Component_React> action) where T : Component
        {
            if (!_handlers.TryGetValue(typeof(T), out var handler))
            {
                _handlers.Add(typeof(T), new Handler<Trigger_Component_React>());
            }
            handler.Subscribe(action);
        }

        public void UnsubscribeOnTrigger<T>(Action<Trigger_Component_React> action) where T : Component
        {
            if (_handlers.TryGetValue(typeof(T), out var handler))
            {
                handler.Unsubscribe(action);
            }
        }
    }

    public readonly struct Trigger_Component_React
    {
        public readonly Collider2D Other;
        public readonly Component  Component;
        public readonly bool       IsEnter;

        public Trigger_Component_React(Collider2D other, Component component, bool isEnter)
        {
            Other = other;
            Component = component;
            IsEnter = isEnter;
        }
    }

    public readonly struct Trigger_React
    {
        public readonly Collider2D Other;
        public readonly bool       IsEnter;

        public Trigger_React(Collider2D other, bool isEnter)
        {
            Other = other;
            IsEnter = isEnter;
        }
    }
}