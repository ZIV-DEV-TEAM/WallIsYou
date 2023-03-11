using System;
using UI;
using UnityEngine;

namespace Input
{
    public class InputService : MonoBehaviour
    {
        [SerializeField] private Play play;
        private Action<Vector3> _joystick;
        private Action<Vector3> _arrows;
        private void Update()
        {
            Vector3 rotateLeftDirection = play.Control.LeftRotation.RotateDirection;
            if (rotateLeftDirection != Vector3.zero)
            {
                _arrows.Invoke(rotateLeftDirection);
            }
            else
            {
                Vector3 rotateRightDirection = play.Control.RightRotation.RotateDirection;
                if (rotateRightDirection != Vector3.zero)
                {
                    _arrows.Invoke(rotateRightDirection);
                }
            }
            Vector3 moveDirection = play.Control.FixedJoystick.Direction;
            if (moveDirection != Vector3.zero)
            {
                _joystick.Invoke(moveDirection);
            }
        }
        public void SubscribeToJoystick(Action<Vector3> subscriber)
        {
            _joystick += subscriber;
        }
        public void UnsubscribeFromJoystick(Action<Vector3> subscriber)
        {
            _joystick -= subscriber;
        }
        public void SubscribeToArrows(Action<Vector3> subscriber)
        {
            _arrows+=subscriber;    
        }
        public void UnsubscribeFromArrows(Action<Vector3> subscriber)
        {
            _arrows -= subscriber;
        }
    }
}
