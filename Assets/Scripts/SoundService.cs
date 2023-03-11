using System;
using UnityEngine;
using UnityEngine.Audio;

public class SoundService : MonoBehaviour
{
    private const string Music = "Music";
    private const string Effects = "Effects";
    [SerializeField] private AudioMixerGroup audioMixerGroup;

    private void Start()
    {
        if (PlayerPrefs.HasKey(Music))
            audioMixerGroup.audioMixer.SetFloat(Music, PlayerPrefs.GetInt(Music));
        if (PlayerPrefs.HasKey(Effects))
            audioMixerGroup.audioMixer.SetFloat(Effects, PlayerPrefs.GetInt(Effects));
    }

    private void Update()
    {
        audioMixerGroup.audioMixer.GetFloat(Music, out var musicValue);
        if ((int)musicValue != PlayerPrefs.GetInt(Music))
            audioMixerGroup.audioMixer.SetFloat(Music, musicValue);
        audioMixerGroup.audioMixer.GetFloat(Music, out var effectValue);
        if ((int)effectValue != PlayerPrefs.GetInt(Effects))
            audioMixerGroup.audioMixer.SetFloat(Effects, PlayerPrefs.GetInt(Effects));
    }
}