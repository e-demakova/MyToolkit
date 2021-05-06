using UnityEngine.UI;
using UnityEngine;

using TMPro;
using Deblue.Localization;

namespace Deblue.DialogSystem
{
    [DefaultExecutionOrder(-900)]
    [RequireComponent(typeof(Animator))]
    public class DialogVisualization : UniqMono<DialogVisualization>
    {
        [SerializeField] private TextMeshProUGUI  _dialogText;
        [SerializeField] private Image            _character;
        [SerializeField] private CharactersDataSO _charactersData;

        private Animator   _animator;
        private static int _isOpen = Animator.StringToHash("IsOpen");

        protected override void MyAwake()
        {
            _animator = GetComponent<Animator>();
#if UNITY_EDITOR
            _charactersData.Serialize();
#endif
        }

        private void OnEnable()
        {
            DialogSwitcher.Events.SubscribeOnReplicaSwitch(VisualizeNewReplica);
            DialogSwitcher.Events.SubscribeOnDialogStart(OpenWindow);
            DialogSwitcher.Events.SubscribeOnDialogEnd(CloseWindow);
        }

        private void OnDisable()
        {
            DialogSwitcher.Events.UnsubscribeOnReplicaSwitch(VisualizeNewReplica);
            DialogSwitcher.Events.UnsubscribeOnDialogStart(OpenWindow);
            DialogSwitcher.Events.UnsubscribeOnDialogEnd(CloseWindow);
        }

        private void CloseWindow(Dialog_End context)
        {
            _animator.SetBool(_isOpen, false);
        }

        private void OpenWindow(Dialog_Start context)
        {
            _animator.SetBool(_isOpen, true);
        }

        private void VisualizeNewReplica(Replica_Switch context)
        {
            _dialogText.text = Localizator.GetText(context.Replica.TextId);
            var data = _charactersData.GetCharacter(context.Replica.CharacterId);
            _character.sprite = data.Icon;
        }
    }
}