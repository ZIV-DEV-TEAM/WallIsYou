using UI.PausePanel;
using UnityEngine;
using UnityEngine.Events;

namespace UI
{
    public class Pause: BasePanel, IInit<PauseDelegate>, IInit<UnityAction<bool>>
    {
        [SerializeField] private Toggles toggles;
        private event PauseDelegate _pause;

        protected override void OnContinue()
        {
            _pause?.Invoke();
        }

        public void Initialize(PauseDelegate @delegate)
        {
            _pause += @delegate;
        }

        public void Initialize(UnityAction<bool> @delegate)
        {
            toggles.SubscribeVibration(@delegate);
        }
    }
}