using Level;
using Player;
using UnityEngine;
using UnityEngine.Events;

namespace UI
{
    public class CanvasController : MonoBehaviour
    {
        [SerializeField] private CanvasGroup playPanel;
        [SerializeField] private CanvasGroup pausePanel;
        [SerializeField] private CanvasGroup gameOverPanel;
        [SerializeField] private CanvasGroup winPanel;
        [SerializeField] private CanvasGroup leaderBoardPanel;
        [SerializeField] private CanvasGroup namePanel;
        [SerializeField] private PlayerAction player;
        [SerializeField] private LevelControl levelControl;
        private Vibration _vibration;

        private IInit<Die> _initDie;
        private IInit<PauseDelegate> _initPause;
        private IInit<Reborn> _initReborn;
        private IInit<UnityAction> _initAction;
        private IInit<UnityAction<bool>> _initBoolAction;
        private IInit<WinDelegate> _initWin;

        private Die _dieDelegate;
        private PauseDelegate _pauseDelegate;
        private WinDelegate _winDelegate;
        private ChangePanel _changePanel;

        private event LeaderboardDelegate _leaderboard;
        private event PauseDelegate _pause;


        private void Start()
        {
            _changePanel = new ChangePanel();
            _vibration = new Vibration();

            InitializeDelegates();
            InitializeSubscribers();
            CheckNewPlayer();
        }

        private void CheckNewPlayer()
        {
            var isNew = PlayerPrefs.HasKey("isNew");
            if (isNew)
            {
                OnPlayPanel();
            }
            else
            {
                PlayerPrefs.SetString("isNew", "");
                _pause?.Invoke();
                _changePanel.SetPanel(namePanel);
            }
        }

        private void InitializeDelegates()
        {
            _dieDelegate = OnGameOverPanel;
            _pauseDelegate = OnPausePanel;
            _winDelegate = WinPanel;
        }

        private void InitializeSubscribers()
        {
            _initDie = player;
            _initDie.Initialize(_vibration.TriggerVibrate);
            _initDie.Initialize(_dieDelegate);

            namePanel.TryGetComponent(out _initAction);
            _initAction.Initialize(OnPlayPanel);
            _initAction.Initialize(() => _pause?.Invoke());

            playPanel.TryGetComponent(out _initPause);
            _initPause.Initialize(player.PauseDelegate);
            _initPause.Initialize(_pauseDelegate);

            pausePanel.TryGetComponent(out _initPause);
            _initPause.Initialize(player.PauseDelegate);
            _initPause.Initialize(OnPlayPanel);

            pausePanel.TryGetComponent(out _initBoolAction);
            _initBoolAction.Initialize(_vibration.OnVibration);

            gameOverPanel.TryGetComponent(out _initReborn);
            _initReborn.Initialize(player.RebornDelegate);
            _initReborn.Initialize(OnPlayPanel);

            if (levelControl != null && levelControl.gameObject.activeInHierarchy)
            {
                _initWin = levelControl;
                _initWin.Initialize(_winDelegate);
            }
            
            leaderBoardPanel.TryGetComponent(out Leaderboard leaderboard);
            _leaderboard += leaderboard.LeaderboardDelegate;
            _pause += player.PauseDelegate;
        }

        private void OnPlayPanel()
        {
            _changePanel.SetPanel(playPanel);
        }

        private void OnPausePanel()
        {
            _changePanel.SetPanel(pausePanel);
        }


        private void OnGameOverPanel()
        {
            _changePanel.SetPanel(gameOverPanel);
        }

        private void WinPanel()
        {
            OnLeaderboard();
            _leaderboard?.Invoke(OnWinPanel, player.DeathBank.Value);
        }

        private void OnWinPanel()
        {
            _changePanel.SetPanel(winPanel);
        }

        private void OnLeaderboard()
        {
            _changePanel.SetPanel(leaderBoardPanel);
        }
    }

    public delegate void WinDelegate();

    public delegate void LeaderboardDelegate(UnityAction action, int score);
}