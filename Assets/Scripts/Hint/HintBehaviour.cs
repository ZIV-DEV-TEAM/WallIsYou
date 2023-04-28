using System;
using ObstacleLogic;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
using DG.Tweening.Core;

public class HintBehaviour : MonoBehaviour
{
    private const string HintAnimation = "HintAnimation";

    [Header("Configurable parameters")] [SerializeField]
    private float deviation;

    [SerializeField] private float animationDuration = 1;
    [Header("Other")] [SerializeField] private MeshFilter meshFilter;
    [SerializeField] private Renderer meshRenderer;
    [SerializeField] private Material material;

    private ObstacleService _obstacleService;
    private bool _isDestroyed;

    public void StartAnimation()
    {
        meshRenderer.material.DOFade(1, animationDuration)
            .OnComplete(() =>
                meshRenderer.material.DOFade(0, animationDuration))
                    .SetId(HintAnimation);
    }

    public void PauseAnimation(bool isPaused)
    {
        if (!isPaused)
        {
            DOTween.Play(HintAnimation);
        }
        else
        {
            DOTween.Pause(HintAnimation);
        }
    }

    public void KillAnimation()
    {
        DOTween.Kill(HintAnimation);
    }

    public void SetObstacleService(ObstacleService obstacleService)
    {
        _obstacleService = obstacleService;
    }

    public void DestroyHint()
    {
        DOTween.Kill(HintAnimation);
        if (!_isDestroyed)
        {
            Destroy(gameObject);
        }
    }

    public void OnPlayerChangedPosition(Vector3 position)
    {
        transform.position = new Vector3(position.x, position.y, transform.position.z);
    }

    public void OnPlayerChangedMesh(Mesh newMesh)
    {
        meshFilter.mesh = newMesh;
    }

    public void OnObstacleChanged(float ZPosition)
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, ZPosition + deviation);
    }

    public HintBehaviour Clone()
    {
        return Instantiate(this);
    }

    private void OnDestroy()
    {
        _isDestroyed = true;
        _obstacleService.ObtacleSwitch -= OnObstacleChanged;
        meshRenderer.material.color = Color.cyan;
    }
}