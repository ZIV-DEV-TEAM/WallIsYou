using UnityEngine;

namespace ObstacleLogic
{
    public class MoveObstacle : MonoBehaviour
    {
        public int ObstacleQuantity = 1;
        [SerializeField] private float moveSpeed;
        [SerializeField] private float rotateSpeed;

        public void OnMove(Vector3 moveDirection)
        {
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);
        }
        public void OnRotate(Vector3 rotateDirection)
        {
            transform.Rotate(rotateDirection, rotateSpeed);
        }
    }
}
