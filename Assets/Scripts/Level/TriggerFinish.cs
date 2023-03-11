using UI;
using UnityEngine;

namespace Level
{
    public class TriggerFinish : MonoBehaviour
    {
        private string _currentLevel = "CurrentLevel";

        private event WinDelegate _win;
    

        public void SubscribeWin(WinDelegate winDelegate)
        {
            _win += winDelegate;
        }

        private void OnTriggerEnter(Collider other)
        {
            int currentLevel =  PlayerPrefs.GetInt(_currentLevel);
            currentLevel++;
            Debug.Log("Restart scene");
            PlayerPrefs.SetInt(_currentLevel, currentLevel);
            _win.Invoke();
       
        }
    }
}
