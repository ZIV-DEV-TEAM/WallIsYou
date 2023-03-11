using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

public class TriggerPosition : MonoBehaviour
{
    [SerializeField] private int idPoint;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IInteractable interactable))
        {
            interactable.SetPosition(idPoint);
        }
    }
}
