using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintAnimationTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IInteractable player))
        {
            player.OnСolliderWithHintTrigger();
        }
    }
}
