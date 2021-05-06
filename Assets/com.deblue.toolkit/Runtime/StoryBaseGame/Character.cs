using UnityEngine;

using Deblue.ObservingSystem;

namespace Deblue.DialogSystem
{
    public readonly struct Player_Near_On_Character
    {
        public readonly Character Character;

        public Player_Near_On_Character(Character character)
        {
            Character = character;
        }
    }

    public readonly struct Player_Exit_On_Character
    {
        public readonly Character Character;

        public Player_Exit_On_Character(Character character)
        {
            Character = character;
        }
    }

    public readonly struct Player_Give_Item
    {
        public readonly Character Character;
        public readonly string    ItemId;

        public Player_Give_Item(Character character, string itemId)
        {
            Character = character;
            ItemId = itemId;
        }
    }

    [RequireComponent(typeof(Collider2D))]
    public class Character : MonoBehaviour
    {
        [SerializeField] private string    _characterId;

        private Handler<Player_Near_On_Character> _playerTalk = new Handler<Player_Near_On_Character>();
        private Handler<Player_Exit_On_Character> _playerExit = new Handler<Player_Exit_On_Character>();

        public IReadOnlyHandler<Player_Near_On_Character> PlayerTalk => _playerTalk;
        public IReadOnlyHandler<Player_Exit_On_Character> PlayerExit => _playerExit;
        public string CharacterId => _characterId;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<Player>(out var player))
            {
                _playerTalk.Raise(new Player_Near_On_Character(this));
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent<Player>(out var player))
            {
                _playerExit.Raise(new Player_Exit_On_Character(this));
            }
        }
    }
}