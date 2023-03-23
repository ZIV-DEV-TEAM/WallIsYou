using System.Collections.Generic;
using Input;
using UnityEngine;
using UnityEngine.Events;

namespace ObstacleLogic
{
    public class ObstacleService : MonoBehaviour
    {
        public event UnityAction<float> ObtacleSwitch;
        [SerializeField] private List<MoveObstacle> obstacles;
        [SerializeField] private List<CompleteTrigger> completeTriggers;
        [SerializeField] private MoveObstacle[] curentObstacles;
        private InputService _inputService;


        public void SetInputService(InputService inputService)
        {
            _inputService = inputService;
        }
    
        public float GetCurentZPosition()
        {
            return curentObstacles[0].transform.position.z;
        }
        private void Start()
        {
            foreach (CompleteTrigger item in completeTriggers)
            {
                item.SubscribeToComplete(OnObstacleComplete);
            }
            foreach (MoveObstacle item in curentObstacles)
            {
                _inputService.SubscribeToArrows(item.OnRotate);
                _inputService.SubscribeToJoystick(item.OnMove);
            }
        }

        private void OnObstacleComplete()
        {
            foreach (var item in curentObstacles)
            {
                _inputService.UnsubscribeFromArrows(item.OnRotate);
                _inputService.UnsubscribeFromJoystick(item.OnMove);
                Destroy(item.gameObject);
            }
            if (obstacles.Count <= 0)
            {
                return;
            }
            MoveObstacle[] newCurrentObstacles = new MoveObstacle[obstacles[0].ObstacleQuantity];
            for (int i = 0; i < newCurrentObstacles.Length; i++)
            {
                newCurrentObstacles[i] = obstacles[0];
                _inputService.SubscribeToArrows(newCurrentObstacles[i].OnRotate);
                _inputService.SubscribeToJoystick(newCurrentObstacles[i].OnMove);
                obstacles.RemoveAt(0);
            }
            curentObstacles = newCurrentObstacles;
            ObtacleSwitch?.Invoke(curentObstacles[0].transform.position.z);
        }
        
        private void OnDestroy()
        {

            foreach (CompleteTrigger item in completeTriggers)
            {
                item.UnsubscribeFromComplete(OnObstacleComplete);
            }
        }
    }
}
