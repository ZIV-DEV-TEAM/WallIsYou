using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopAllAudioFromTriggers : MonoBehaviour
{
    [SerializeField] private AudioSource[] audioWithTrigger;
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent(out IInteractable player))
        {
            foreach (var item in audioWithTrigger)
            {
                item.Pause();
            }
            player.PlayerReborn += StartMusic;
        }
    }
    private void StartMusic()
    {
        foreach (var item in audioWithTrigger)
        {
            item.Play();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out IInteractable player))
        {
            foreach (var item in audioWithTrigger)
            {
                item.Stop();
            }
        }
    }
}
