using UnityEngine;

namespace Deblue.Interactive
{
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    public abstract class InteractiveObject : MonoBehaviour
    {
        [System.Serializable]
        protected struct SpritePair
        {
            public Sprite Standart;
            public Sprite Highlight;
        }

        protected abstract bool CanHighlight { get; }

        public SpriteRenderer Renderer { get; private set; }
        public int DefoultSortOrder { get; private set; }
        public Quaternion DefoultRotation { get; private set; }

        [SerializeField] protected SpriteRenderer _keyView;

        protected Collider2D _collider;
        protected bool       _isHighlight;
        protected bool       _isTaken;
        protected bool       _isPlayerNear;

        protected void Awake()
        {
            _collider = GetComponent<Collider2D>();
            Renderer = GetComponent<SpriteRenderer>();
            DefoultSortOrder = Renderer.sortingOrder;
            DefoultRotation = transform.rotation;
            MyAwake();
        }

        protected virtual void MyAwake()
        {

        }

        protected void TryHilight()
        {
            if (CanHighlight && _isPlayerNear)
            {
                _keyView.enabled = true;
                Highlight();
            }
            else
            {
                StopHighlight();
            }
        }

        protected abstract void Highlight();
        protected abstract void StopHighlight();
    }

    public abstract class TakebleObject : InteractiveObject, ITakebleObject
    {
        public abstract bool CanPut { get; }
        public abstract bool CanTake { get; }

        public TakebleObject Take()
        {
            _isTaken = true;
            StopHighlight();
            return this;
        }

        public void Put()
        {
            _isTaken = false;
            TryHilight();
        }
    }

    public abstract class TakebleObjectContainer : InteractiveObject, ITakebleObjectContainer
    {
        public abstract bool CanReturn { get; }
        public abstract bool CanTake { get; }

        public abstract TakebleObject Take();
        public abstract void Return();
    }
}