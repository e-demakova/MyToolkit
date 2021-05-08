using System.Collections.Generic;

using UnityEngine;

using Deblue.ObservingSystem;
using Deblue.InputSystem;
using Deblue.GameProcess;

namespace Deblue.DialogSystem
{
    [System.Serializable]
    public class AvalibleCharacterOnStep
    {
        public string   CharacterId;
        public DialogSO Dialog;
        public bool     IsImportant;

        [System.NonSerialized] public bool IsTalcked;
    }

    public class DialogRequester
    {
        private DialogSwitcher                        _switcher;
        private ObservDictionary<Character, DialogSO> _dialogues = new ObservDictionary<Character, DialogSO>();
        private Character                             _currentCharacter;
        private AvalibleCharacterOnStep[]             _avalibleCharacters;
        private Character[]                           _charactersInScene;
        private List<IObserver>                       _observCharacters = new List<IObserver>(5);
        private List<IObserver>                       _observers = new List<IObserver>(5);

        private bool _isPaused;

        public DialogRequester(DialogSwitcher switcher)
        {
            _switcher = switcher;
            InputReciver.SubscribeOnInput<On_Button_Down>(RequestDialog, KeyCode.E, _observers);
            GameTime.SubscribeOnPause(context => _isPaused = context.IsPaused, _observers);
        }

        public void DeInit()
        {
            Unsubscribe(_observCharacters);
            Unsubscribe(_observers);
        }

        public void SetAvalibleDialogues(AvalibleCharacterOnStep[] avalibleCharacters)
        {
            _avalibleCharacters = avalibleCharacters;
            UpdateCharactersOnNewScene();
            UpdateAvalibleCharacters();
        }

        private void UpdateAvalibleCharacters()
        {
            _dialogues.Clear();
            for (int i = 0; i < _avalibleCharacters.Length; i++)
            {
                var character = _avalibleCharacters[i];
                if (character.IsTalcked)
                {
                    continue;
                }
                for (int j = 0; j < _charactersInScene.Length; j++)
                {
                    var characterInScene = _charactersInScene[j];
                    if (character.CharacterId == characterInScene.CharacterId)
                    {
                        _dialogues.Add(characterInScene, character.Dialog);

                        characterInScene.PlayerTalk.Subscribe(context =>
                        {
                            if (context.IsEnter)
                            {
                                SetCharacter(context.Character);
                            }
                            else
                            {
                                _currentCharacter = null;
                            }
                        }, _observCharacters);

                        break;
                    }
                }
            }
        }

        private void RequestDialog(On_Button_Down context)
        {
            if (_currentCharacter == null)
            {
                return;
            }
            if (_dialogues.TryGetValue(_currentCharacter, out var dialog) && !_isPaused)
            {
                _switcher.StartDialog(dialog, _currentCharacter);
            }
        }

        private void SetCharacter(Character character)
        {
            _currentCharacter = character;
        }

        private void UpdateCharactersOnNewScene()
        {
            _charactersInScene = Object.FindObjectsOfType<Character>();
            Unsubscribe(_observCharacters);
            UpdateAvalibleCharacters();
        }

        private void Unsubscribe(List<IObserver> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                list[i].Dispose();
            }
            list.Clear();
        }
    }
}