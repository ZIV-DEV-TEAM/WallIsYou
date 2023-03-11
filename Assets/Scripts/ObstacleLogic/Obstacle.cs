using UnityEngine;

namespace ObstacleLogic
{
    public abstract class Obstacle : MonoBehaviour
    {
        public abstract void OnTriggerEnter(Collider other);
    }
}
