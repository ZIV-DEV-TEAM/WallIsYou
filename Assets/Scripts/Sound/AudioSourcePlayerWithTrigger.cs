using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourcePlayerWithTrigger : MonoBehaviour
{
    [SerializeField] private AudioSource[] audioSourcesForPlay;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<IInteractable>(out IInteractable player))
        {
            foreach (var item in audioSourcesForPlay)
            {
                item.Play();
            }
        }
    }
    public void StopAllAudio()
    {
        foreach (var item in audioSourcesForPlay)
        {
            item.Stop();
        }
    }
}
