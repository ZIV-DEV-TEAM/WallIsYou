using UI;
using UnityEngine;

namespace Level
{
    public class TriggerFinish : MonoBehaviour
    {
        private string _currentLevel = "CurrentLevel";

        private event WinDelegate _win;

        private bool _isFinished;
        public void SubscribeWin(WinDelegate winDelegate)
        {
            _win += winDelegate;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_isFinished)
                return;
            _isFinished = true;
            int currentLevel =  PlayerPrefs.GetInt(_currentLevel);
            currentLevel++;
            Debug.Log("Restart scene");
            PlayerPrefs.SetInt(_currentLevel, currentLevel);
            _win.Invoke();
       
        }
    }
}
