using Input;
using ObstacleLogic;
using Player;
using UI;
using UnityEngine;

namespace Level
{
    public class LevelControl : MonoBehaviour, IInit<WinDelegate>
    {
        [SerializeField] private LevelLinks[] levels;
        [SerializeField] private InputService inputService;
        [SerializeField] private PlayerAction player;
        private string _currentLevel = "CurrentLevel";
        private LevelLinks _currentLevelInstance;

        private void Awake()
        {

            if (!PlayerPrefs.HasKey(_currentLevel))
            {
                PlayerPrefs.SetInt(_currentLevel, 0);
                Instantiate(PlayerPrefs.GetInt(_currentLevel));
            }
            else
            {
                Instantiate(PlayerPrefs.GetInt(_currentLevel));
            }
        }


        private void Instantiate(int level)
        {
            if (level >= levels.Length)
            {
                level = 0;
                PlayerPrefs.SetInt(_currentLevel, level);
            }
            _currentLevelInstance= Instantiate(levels[level]);
            _currentLevelInstance.TryGetComponent(out ObstacleService service);
            service.SetInputService(inputService);
            player.SetPositionController(_currentLevelInstance.PositionController);
        }
        
        

        public void Initialize(WinDelegate @delegate)
        {
            _currentLevelInstance.TriggerFinish.SubscribeWin(@delegate);
        }
    }
}
