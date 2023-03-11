using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.PlayPanel
{
    public class RotateWithTheArrows : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private bool _isRight;
        private Vector3 _rotateDirection;
        public Vector3 RotateDirection => _rotateDirection;
        public void OnPointerDown(PointerEventData eventData)
        {
            if (_isRight)
            {
                _rotateDirection = Vector3.back;
            }
            else
            {
                _rotateDirection = Vector3.forward;
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _rotateDirection = Vector3.zero;
        }
    }
}