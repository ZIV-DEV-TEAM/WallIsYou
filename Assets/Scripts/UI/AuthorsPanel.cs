using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AuthorsPanel : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private Button closeButton;
    [SerializeField] private float duration;
    private void Awake()
    {
        closeButton.onClick.AddListener(Deactivate);
    }
    public void Activate() 
    {
        canvasGroup.DOFade(1, duration);
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }
    public void Deactivate()
    {
        canvasGroup.DOFade(0, duration);
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false; 
    }
}
