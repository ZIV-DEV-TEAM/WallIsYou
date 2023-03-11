using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPlayer : MonoBehaviour
{
    private bool _isDestroyed;
    private void OnTriggerEnter(Collider other)
    {
        if (_isDestroyed)
        {
            return;
        }
        if (other.TryGetComponent(out IInteractable interactable))
        {
            _isDestroyed = true;
            Destroy(other.gameObject);
        }
    }
}

