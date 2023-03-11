using Joystick_Pack.Scripts.Base;
using UnityEngine;

namespace UI.PlayPanel
{
    public class Control : MonoBehaviour
    {
        [SerializeField] private Joystick fixedJoystick;
        [SerializeField] private RotateWithTheArrows leftRotation;
        [SerializeField] private RotateWithTheArrows rightRotation;

        public Joystick FixedJoystick => fixedJoystick;
        public RotateWithTheArrows LeftRotation => leftRotation;
        public RotateWithTheArrows RightRotation => rightRotation;
    }
}