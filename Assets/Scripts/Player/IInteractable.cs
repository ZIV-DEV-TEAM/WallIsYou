
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
        public void Die();
        public void AddScore(int score);
        public void SetPosition(int key);
        public void SetMesh(Mesh newMesh);
        public IInteractable Clone(Mesh newMesh, int key);
    }
}