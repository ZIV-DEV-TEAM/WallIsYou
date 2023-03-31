using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI.PausePanel
{
    public class Toggles : MonoBehaviour
    {
        private const string VibrateText = "Vibrate";
        private const string Music = "Music";
        private const string Effects = "Effects";
        [SerializeField] private Toggle effect;
        [SerializeField] private Toggle music;
        [SerializeField] private Toggle vibration;

        private void Start()
        {
            if(PlayerPrefs.HasKey(VibrateText))
                vibration.isOn = PlayerPrefs.GetInt(VibrateText) == 1;
            if (PlayerPrefs.HasKey(Music))
                music.isOn = PlayerPrefs.GetInt(Music) == 0;
            if (PlayerPrefs.HasKey(Effects))
                effect.isOn = PlayerPrefs.GetInt(Effects) == 0;
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
