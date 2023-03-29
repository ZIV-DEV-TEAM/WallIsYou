﻿
using UI;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

namespace Player
{
    public interface IInteractable
    {
        public event UnityAction DestroyPlayer;
        public event UnityAction<Mesh> PlayerChangedMesh;
        public event UnityAction<Vector3> PlayerChangedPosition;
        public event UnityAction СolliderWithHintTrigger;
        public event UnityAction<bool> PlayerPause;
        public event UnityAction PlayerReborn;

        public void OnСolliderWithHintTrigger();
        public void RemoveEverywhere();
        public void Die(bool isCallFromOriginal);
        public void AddScore(int score);
        public void SetPosition(int key);
        public void SetMesh(Mesh newMesh);
        public void Pause();
        public IInteractable Clone(Mesh newMesh, int key);
    }
}