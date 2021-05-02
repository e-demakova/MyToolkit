using UnityEngine;

using Deblue.DialogSystem;
using Deblue.Interactive;

namespace Deblue.Story
{
    [CreateAssetMenu(fileName = "ChangeStepCondition_StartDialog", menuName = "Story/Change Step Conditions/Start Dialog")]
    public class ChangeStepOnDialogSO : ChangeStepConditionSO
    {
        [SerializeField] private string _targetCharacterId;

        protected override void MyInit()
        {
            DialogSwitcher.Events.SubscribeOnDialogStart(OnDone);
        }

        protected override void DeInit()
        {
            DialogSwitcher.Events.UnsubscribeOnDialogStart(OnDone);
        }

        private void OnDone(Dialog_Start context)
        {
            if (context.Dialog.CharacterId == _targetCharacterId)
            {
                OnDone();
            }
        }
    }
    
    [CreateAssetMenu(fileName = "ChangeStepCondition_StartDialog", menuName = "Story/Change Step Conditions/Start Dialog")]
    public class ChangeStepOnGiveItemSO : ChangeStepConditionSO
    {
        [SerializeField] private string _targetCharacterId;
        [SerializeField] private string _itemId;

        protected override void MyInit()
        {
        }

        protected override void DeInit()
        {
        }
    }
}