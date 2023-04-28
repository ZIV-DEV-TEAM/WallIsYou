using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public abstract class BasePanel : MonoBehaviour
    {
        [SerializeField] protected Button restartButton;
        [SerializeField] protected Button continueButton;
        [SerializeField] protected Button authorsButton;
        [SerializeField] protected AuthorsPanel authorsPanel;
        protected virtual void Start()
        {
            restartButton.onClick.AddListener(OnRestart);
            authorsButton.onClick.AddListener(OnAuthorsClick);
            if (continueButton != null)
                continueButton.onClick.AddListener(OnContinue);
        }
        private void OnRestart()
        {
            SceneManager.LoadScene("Play");
        }

        protected virtual void OnContinue()
        {
            Debug.Log("Continue");
        }
        protected virtual void OnAuthorsClick()
        {
            authorsPanel.Activate();
        }
    }
}
