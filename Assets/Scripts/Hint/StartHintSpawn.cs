using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartHintSpawn : MonoBehaviour
{
    [SerializeField] private SpawnHint spawner;
    [SerializeField] private Mesh meshClone;
    [SerializeField] private int idPoint;
    private bool _isSpawned;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IInteractable interactable))
        {
            if (_isSpawned)
                return;
            spawner.SpawnHintForPlayer(meshClone, idPoint, interactable);
            _isSpawned = true;
        }
    }
}
