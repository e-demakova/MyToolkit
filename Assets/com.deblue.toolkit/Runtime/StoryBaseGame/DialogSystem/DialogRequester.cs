using System.Collections.Generic;

using UnityEngine;

using Deblue.ObservingSystem;

namespace Deblue.DialogSystem
{
    [System.Serializable]
    public class AvalibleCharacterOnStep
    {
        public string   CharacterId;
        public DialogSO Dialog;
        public bool     IsTalcked;
    }

    public class DialogRequester
    {
        private DialogSwitcher _switcher;
        private Character[] _charactersInScene;
        private AvalibleCharacterOnStep[] _avalibleCharacters;

        private ObservDictionary<Character, DialogSO> _dialogues;
        private List<IObserver> _observCharacters = new List<IObserver>(5);

        public DialogRequester(DialogSwitcher switcher)
        {
            _switcher = switcher;
        }

        public void DeInit()
        {
            UnsubscribeOnCharacters();
        }

        public void SetAvalibleDialogues(AvalibleCharacterOnStep[] avalibleCharacters)
        {
            _avalibleCharacters = avalibleCharacters;
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
                for (int j = 0;  j < _charactersInScene.Length;  j++)
                {
                    var characterInScene = _charactersInScene[j];
                    if (character.CharacterId == characterInScene.CharacterId)
                    {
                        _dialogues.Add(characterInScene, character.Dialog);
                        characterInScene.PlayerTryTalk.Subscribe(RequestDialog, _observCharacters);
                        break;
                    }
                }
            }
        }

        private void RequestDialog(Player_Try_Talk coontext)
        {

        }

        private void UpdateCharactersOnNewScene()
        {
            UnsubscribeOnCharacters();
            _charactersInScene = Object.FindObjectsOfType<Character>();
            UpdateAvalibleCharacters();
        }

        private void UnsubscribeOnCharacters()
        {
            for (int i = 0; i < _observCharacters.Count; i++)
            {
                _observCharacters[i].Dispose();
            }
        }
    }
}