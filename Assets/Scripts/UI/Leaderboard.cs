using OnlineLeaderboards;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI
{
    public class Leaderboard : MonoBehaviour
    {
        
        [SerializeField] private Button closeButton;
        [SerializeField] private LeaderboardController leaderboard;
        [SerializeField] private PlayerInfo playerInfo;
        private LeaderboardDelegate _leaderboardDelegate;
        private PlayerInfoInstantiated _playerInfoInstantiated;
        public LeaderboardDelegate LeaderboardDelegate => _leaderboardDelegate;

        private void Awake()
        {
            _leaderboardDelegate = SubscribeClose;
            _playerInfoInstantiated = PlayerInfoInstantiate;
            leaderboard.SubscribeInstantiate(_playerInfoInstantiated);
        }

        private void SubscribeClose(UnityAction action, int dieCount)
        {
            closeButton.onClick.RemoveAllListeners();
            closeButton.onClick.AddListener(action);
            StartCoroutine(leaderboard.SubmitScoreRoutine(dieCount));
        }

        private void PlayerInfoInstantiate(string name, string dieCount)
        {
            playerInfo.SetInfo(name,dieCount);
        }
    }

    public delegate void PlayerInfoInstantiated(string name,string dieCount);
}
