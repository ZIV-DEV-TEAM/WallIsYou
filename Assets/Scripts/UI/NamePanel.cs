using System.Collections.Generic;
using OnlineLeaderboards;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI
{
    public class NamePanel : MonoBehaviour, IInit<UnityAction>
    {
        [SerializeField] private Button saveButton;
        [SerializeField] private TMP_InputField playerID;
        [SerializeField] private LeaderboardController leaderboardController;

        private List<UnityAction> _actions;

        private void Start()
        {
            _actions = new List<UnityAction>();
            saveButton.onClick.AddListener(SetID);
        }

        private void SetID()
        {
            PlayerPrefs.SetString("PlayerID", playerID.text);
            leaderboardController.SetPlayerName();
            foreach (var action in _actions)
            {
                action?.Invoke();
            }
        }

        public void Initialize(UnityAction @delegate)
        {
            _actions.Add(@delegate);
        }
    }
}