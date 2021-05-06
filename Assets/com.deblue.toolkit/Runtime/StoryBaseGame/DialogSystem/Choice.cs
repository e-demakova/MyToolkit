using UnityEngine;

namespace Deblue.DialogSystem
{
    [System.Serializable]
    public class Choice
    {
        [System.NonSerialized] public bool IsAvalible;

        [SerializeField] private DialogSO _nextDialogue;
        [SerializeField] private string   _answerId;
        [SerializeField] private string   _blockReasonTextId;
        [SerializeField] private string   _itemId;

        public string ItemID => _itemId;
        public string Response => _answerId;
        public DialogSO NextDialogue => _nextDialogue;
    }
}