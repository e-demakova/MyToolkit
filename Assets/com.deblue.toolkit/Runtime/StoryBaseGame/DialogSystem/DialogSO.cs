using System;

using UnityEngine;

namespace Deblue.DialogSystem
{
    [CreateAssetMenu(fileName = "New_Dialog", menuName = "Dialog system/Dialog")]
    public class DialogSO : ScriptableObject
    {
        public Choice[] Choices = default;

        [SerializeField] private Replica[]       _elements;
        [SerializeField] private string          _characterId;
        [SerializeField] private DialogStartType _startType;

        [NonSerialized] private int _elementIndex = -1;

        public DialogStartType StartType => _startType;
        public string CharacterId => _characterId;
        public bool IsHaveChoices => Choices?.Length > 0;

        public void Init()
        {
#if UNITY_EDITOR
            _elementIndex = -1;
#endif
        }

        public bool TrySwitchToNextReplica(out Replica replica)
        {
            replica = null;
            if (ElementsEnded())
            {
                return false;
            }
            _elementIndex++;
            replica = _elements[_elementIndex];
            return true;
        }

        public bool ElementsEnded()
        {
            return _elementIndex >= _elements.Length - 1;
        }
    }

    public enum DialogStartType
    {
        Forcibly,
        Voluntarily
    }
}