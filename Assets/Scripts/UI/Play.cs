using UI.PlayPanel;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public delegate void PauseDelegate();

    public class Play : MonoBehaviour, IInit<PauseDelegate>
    {
        [SerializeField] private Button pauseButton;
        [SerializeField] private Control control;
        private event PauseDelegate _pause;
        public Control Control => control;


        public void Awake()
        {
            pauseButton.onClick.AddListener(() => _pause?.Invoke());
        }

        public void Initialize(PauseDelegate @delegate)
        {
            _pause += @delegate;
        }
    }
}