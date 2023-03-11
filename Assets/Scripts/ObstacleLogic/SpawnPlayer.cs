using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpawnPlayer : MonoBehaviour,IInit<UnityAction<Mesh, int, IInteractable>>
{
    public event UnityAction<Mesh, int, IInteractable> OnPlayerSpawned;
    [SerializeField] private Mesh meshClone;
    [SerializeField] private int idPoint;
    private bool _isUsed;
    private void OnTriggerEnter(Collider other)
    {
        if (_isUsed)
        {
            return;
        }
        if (other.TryGetComponent(out IInteractable interactable))
        {
            _isUsed = true;
            IInteractable player = interactable.Clone(meshClone, idPoint);
            OnPlayerSpawned?.Invoke(meshClone, idPoint, player);
        }
    }

    public void Initialize(UnityAction<Mesh, int, IInteractable> @delegate)
    {
        OnPlayerSpawned += @delegate;
    }
}
