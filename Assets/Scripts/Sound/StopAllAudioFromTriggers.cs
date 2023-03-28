using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopAllAudioFromTriggers : MonoBehaviour
{
    [SerializeField] private AudioSourcePlayerWithTrigger[] audioWithTrigger;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out IInteractable player))
        {
            foreach (var item in audioWithTrigger)
            {
                item.StopAllAudio();
            }
        }
    }
}
