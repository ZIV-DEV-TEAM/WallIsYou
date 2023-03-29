using DG.Tweening;
using System;
using System.Threading;
using UI;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundService : MonoBehaviour
{
    private const string Music = "Music";
    private const string Effects = "Effects";
    [SerializeField] private AudioMixerGroup audioMixerGroup;
    [SerializeField] private int muteValue = -50;
    [SerializeField] private Pause pause;
    [SerializeField] private Button pauseButton;
    private bool _isPaused;

    private void Start()
    {
        pause.Initialize(UnMute);
        pauseButton.onClick.AddListener(Mute);
        if (PlayerPrefs.HasKey(Music))
            audioMixerGroup.audioMixer.SetFloat(Music, PlayerPrefs.GetInt(Music));
        if (PlayerPrefs.HasKey(Effects))
            audioMixerGroup.audioMixer.SetFloat(Effects, PlayerPrefs.GetInt(Effects));
    }

    private void Update()
    {
        audioMixerGroup.audioMixer.GetFloat(Music, out var musicValue);
        if (_isPaused)
            audioMixerGroup.audioMixer.SetFloat(Music, muteValue);
        else if ((int)musicValue != PlayerPrefs.GetInt(Music))
        {
            audioMixerGroup.audioMixer.SetFloat(Music, PlayerPrefs.GetInt(Music));
        }
        audioMixerGroup.audioMixer.GetFloat(Music, out var effectValue);
        if ((int)effectValue != PlayerPrefs.GetInt(Effects))
            audioMixerGroup.audioMixer.SetFloat(Effects, PlayerPrefs.GetInt(Effects));
    }
    public void Mute()
    {
        _isPaused = true;
    }
    public void UnMute()
    {
        _isPaused = false;
    }
}