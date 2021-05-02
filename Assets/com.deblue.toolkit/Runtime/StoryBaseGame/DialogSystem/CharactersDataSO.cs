﻿using System.Collections.Generic;

using UnityEngine;

namespace Deblue.DialogSystem
{
    [CreateAssetMenu(fileName = "CharactersData", menuName = "Dialog system/Characters")]
    public class CharactersDataSO : ScriptableObject, ISerializationCallbackReceiver
    {
        [System.Serializable]
        public struct Character
        {
            public string CharacterID;
            public Sprite Icon;
            public bool   IsPlayer;
        }

        [SerializeField] private Character[] _charactersData;

        private Dictionary<string, Character> _characters = new Dictionary<string, Character>(5);

        public Character GetCharacter(string id)
        {
            if (_characters.TryGetValue(id, out var character))
            {
                return character;
            }
            throw new System.Exception(string.Format("Character id {0} didn't register.", id));
        }

        public void OnBeforeSerialize()
        {
            for (int i = 0; i < _charactersData.Length; i++)
            {
                _characters.Add(_charactersData[i].CharacterID, _charactersData[i]);
            }
        }

        public void OnAfterDeserialize()
        {
            _characters.Clear();
        }
    }
}