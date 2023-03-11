using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

public class TriggerMesh : MonoBehaviour
{
    [SerializeField] private Mesh mesh;
    [SerializeField] private int idPoint = -1;
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
            interactable.SetMesh(mesh);
            if (idPoint!=-1)
            {
                interactable.SetPosition(idPoint);
            }


        }
    }
}
