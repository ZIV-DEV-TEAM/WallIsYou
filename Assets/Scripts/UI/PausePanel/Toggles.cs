using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI.PausePanel
{
    public class Toggles : MonoBehaviour
    {
        [SerializeField] private Toggle effect;
        [SerializeField] private Toggle music;
        [SerializeField] private Toggle vibration;

        private void Start()
        {
            vibration.isOn = PlayerPrefs.GetInt("Vibrate") == 0;
            effect.onValueChanged.AddListener(OnEffect);
            music.onValueChanged.AddListener(OnMusic);
        }

        public void SubscribeVibration(UnityAction<bool> action)
        {
            vibration.onValueChanged.AddListener(action);
        }

        private void OnMusic(bool value)
        {
            PlayerPrefs.SetInt("Music", value?0:-80);
        }

        private void OnEffect(bool value)
        {
            PlayerPrefs.SetInt("Effects", value?0:-80);
        }
    }
}
