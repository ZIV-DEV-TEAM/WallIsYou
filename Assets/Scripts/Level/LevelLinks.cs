using UnityEngine;

namespace Level
{
    public class LevelLinks : MonoBehaviour
    {
        [SerializeField] private TriggerFinish finish;
        [SerializeField] private PositionController positionController;
        public PositionController PositionController => positionController;

        public TriggerFinish TriggerFinish => finish;
    }
}
