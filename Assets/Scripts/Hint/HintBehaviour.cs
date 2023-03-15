using System;
using ObstacleLogic;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class HintBehaviour : MonoBehaviour
{
    [SerializeField] private MeshFilter meshFilter;
    [SerializeField] private Renderer meshRenderer;
    [SerializeField] private Material material;
    [SerializeField] private float deviation;
    private ObstacleService _obstacleService;
    private bool _isDestroyed;

    private void Start()
    {
        meshRenderer.materials = new Material[1]{material};
        material.DOFade(0,1).SetLoops(-1, LoopType.Yoyo);
    }

    public void SetObstacleService(ObstacleService obstacleService)
    {
        _obstacleService = obstacleService;
    }
    public void DestroyHint()
    {
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
        transform.position = new Vector3(transform.position.x, transform.position.y, ZPosition+ deviation);
    }
    public HintBehaviour Clone()
    {
        return Instantiate(this);
    }
    public void RestartAnimation()
    {
        gameObject.SetActive(false);
        gameObject.SetActive(true);
    }
    private void OnDestroy()
    {
        _isDestroyed = true;
        _obstacleService.ObtacleSwitch -= OnObstacleChanged;
        material.color = Color.cyan;
    }
}
