using System;
using System.Collections.Generic;
using Deblue.ObservingSystem;

namespace Deblue.DialogSystem
{
    public class DialogSwitcher : UniqMono<DialogSwitcher>
    {
        public static IDialogSwitcherEvents Events => _events;
        public static bool IsInDialg { get; private set; }

        private static DialogSwitcherEvents _events = new DialogSwitcherEvents();
        private static DialogSO             _currentDialog;
        private static IChoiceReciver       _choiceReciver;

        public void Init(IChoiceReciver choiceReciver)
        {
            _choiceReciver = choiceReciver;
        }

        public void StartDialog(DialogSO dialog)
        {
            _currentDialog = dialog;
            _events.Raise(new Dialog_Start(dialog));
            _currentDialog.Init();
            IsInDialg = true;
            SwitchReplica();
        }

        public void CloseCurrentDialog()
        {
            _events.Raise(new Dialog_End(_currentDialog));
            IsInDialg = false;
            _currentDialog = null;
        }

        public bool TryCloseDialog(DialogSO dialog)
        {
            if (dialog == _currentDialog)
            {
                CloseCurrentDialog();
                return true;
            }
            return false;
        }

        public void SwithReplicaOnButton()
        {
            SwitchReplica();
        }

        public void SwitchReplica()
        {
            if (_currentDialog.TrySwitchToNextReplica(out var replica))
            {
                _events.Raise(new Replica_Switch(replica));
            }
            else if (_currentDialog.IsHaveChoices)
            {
                var choices = _currentDialog.Choices;
                for (int i = 0; i < choices.Length; i++)
                {
                    choices[i].IsAvalible = _choiceReciver.ChoiceAvalile(choices[i]);
                }
                _events.Raise(new Dialog_Give_Choice(choices));
            }
            else
            {
                CloseCurrentDialog();
            }
        }

        public void OnChoiceMade(Choice choice)
        {
            _events.Raise(new Dialog_Choice_Maded(choice));
        }
    }

    public interface IDialogSwitcherEvents
    {
        IObserver SubscribeOnDialogStart(Action<Dialog_Start> action, List<IObserver> observers = null);
        IObserver SubscribeOnDialogEnd(Action<Dialog_End> action, List<IObserver> observers = null);
        IObserver SubscribeOnReplicaSwitch(Action<Replica_Switch> action, List<IObserver> observers = null);
        IObserver SubscribeOnGiveChoice(Action<Dialog_Give_Choice> action, List<IObserver> observers = null);

        void UnsubscribeOnDialogStart(Action<Dialog_Start> action);
        void UnsubscribeOnDialogEnd(Action<Dialog_End> action);
        void UnsubscribeOnReplicaSwitch(Action<Replica_Switch> action);
        void UnsubscribeOnGiveChoice(Action<Dialog_Give_Choice> action);
    }

    public class DialogSwitcherEvents : EventSender, IDialogSwitcherEvents
    {
        #region Subscribing
        public IObserver SubscribeOnDialogStart(Action<Dialog_Start> action, List<IObserver> observers = null)
        {
            return Subscribe(action, observers);
        }

        public IObserver SubscribeOnDialogEnd(Action<Dialog_End> action, List<IObserver> observers = null)
        {
            return Subscribe(action, observers);
        }

        public IObserver SubscribeOnReplicaSwitch(Action<Replica_Switch> action, List<IObserver> observers = null)
        {
            return Subscribe(action, observers);
        }
        
        public IObserver SubscribeOnGiveChoice(Action<Dialog_Give_Choice> action, List<IObserver> observers = null)
        {
            return Subscribe(action, observers);
        }
        #endregion

        #region Unsubscribing
        public void UnsubscribeOnDialogStart(Action<Dialog_Start> action)
        {
            Unsubscribe(action);
        }

        public void UnsubscribeOnDialogEnd(Action<Dialog_End> action)
        {
            Unsubscribe(action);
        }

        public void UnsubscribeOnReplicaSwitch(Action<Replica_Switch> action)
        {
            Unsubscribe(action);
        }
        
        public void UnsubscribeOnGiveChoice(Action<Dialog_Give_Choice> action)
        {
            Unsubscribe(action);
        }
        #endregion
    }
}
