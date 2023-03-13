using ObstacleLogic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class HintBehaviour : MonoBehaviour
{
    private const string Play = "Play";
    [SerializeField] private MeshFilter meshFilter;
    [SerializeField] private float deviation;
    [SerializeField] private Animator animator;
    private ObstacleService _obstacleService;
    private bool isDestroyed;
    public void SetObstacleService(ObstacleService obstacleService)
    {
        _obstacleService = obstacleService;
    }
    public void DestroyHint()
    {
        if (!isDestroyed)
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
        isDestroyed = true;
        _obstacleService.ObtacleSwitch -= OnObstacleChanged;
    }
}
