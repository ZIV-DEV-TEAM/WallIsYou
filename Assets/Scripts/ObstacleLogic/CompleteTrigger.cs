using Player;
using UnityEngine;

namespace ObstacleLogic
{
    public class CompleteTrigger : Obstacle
    {
        public delegate void CompleteHandler();
        event CompleteHandler _complete;
        private bool _isCompleted;
        public override void OnTriggerEnter(Collider other)
        {
            if (_isCompleted)
            {
                return;
            }
            if (other.TryGetComponent(out IInteractable playerAction))
            {
                _isCompleted = true;
                playerAction.AddScore(1);
                _complete?.Invoke();
            }
        }
        public void SubscribeToComplete(CompleteHandler subscriber)
        {
            _complete += subscriber;
        }
        public void UnsubscribeFromComplete(CompleteHandler subscriber)
        {
            _complete -= subscriber;
        }
    }
}
