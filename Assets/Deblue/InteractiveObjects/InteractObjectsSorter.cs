using System.Collections.Generic;
using System.Linq;
using Deblue.Input;
using Deblue.ObservingSystem;
using UnityEngine;
using Zenject;

namespace Deblue.InteractiveObjects
{
    public class InteractObjectsSorter
    {
        private readonly List<InteractItem> _objects = new List<InteractItem>(30);
        private readonly List<IObserver> _observers = new List<IObserver>(30);
        private IInteractItem _highlightedObj;

        private IItemTaker _taker;

        private InteractItem NearInteractItem
        {
            get
            {
                var obj = _objects.FirstOrDefault(t => t.Order == InteractExecutionOrder.First);

                if (!obj)
                    obj = _objects.FirstOrDefault(t => t.Order == InteractExecutionOrder.Second);

                if (!obj)
                    obj = _objects.FirstOrDefault(t => t.Order == InteractExecutionOrder.Third);

                return obj;
            }
        }

        [Inject]
        protected void Construct(InputReceiver inputReceiver)
        {
            _objects.AddRange(Object.FindObjectsOfType<InteractItem>());

            for (int i = 0; i < _objects.Count; i++)
            {
                _objects[i].Updated.Subscribe(OnUpdate, _observers);
            }

            inputReceiver.SubscribeOnInput<ButtonDown>(Interact, KeyCode.F, _observers);
        }

        private void OnDestroy()
        {
            for (int i = 0; i < _observers.Count; i++)
            {
                _observers[i].Dispose();
            }

            _observers.Clear();
        }

        public void Init(IItemTaker taker)
        {
            _taker = taker;
        }

        private void OnUpdate(InteractObjectUpdated context)
        {
            UpdateHighlight();
        }

        private void UpdateHighlight()
        {
            var obj = NearInteractItem;

            if (!obj)
                StopHighlightObj();
            else if (obj.CanHighlight(_taker))
                HighlightObject(obj);
        }

        private void StopHighlightObj()
        {
            _highlightedObj?.StopHighlight();
            _highlightedObj = null;
        }

        private void HighlightObject(IInteractItem obj)
        {
            obj.Highlight();
            _highlightedObj = obj;
        }

        private void Interact(ButtonDown context)
        {
            _highlightedObj?.Interact(_taker);
        }
    }
}