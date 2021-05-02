using UnityEngine;

using Deblue.ObservingSystem;

namespace Deblue.DialogSystem
{
    public struct Player_Try_Talk
    {
    }

    public class Character : MonoBehaviour
    {
        [SerializeField] private string _characterId;

        private Handler<Player_Try_Talk> _playerTryTalk = new Handler<Player_Try_Talk>();

        public IReadOnlyHandler<Player_Try_Talk> PlayerTryTalk => _playerTryTalk;
        public string CharacterId => _characterId;
    }
}