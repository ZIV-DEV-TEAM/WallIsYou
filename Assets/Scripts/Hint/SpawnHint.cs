using ObstacleLogic;
using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnHint : MonoBehaviour
{
    [SerializeField] private SpawnPlayer spawner;
    [SerializeField] private HintBehaviour hint;
    [SerializeField] private PositionController positionController;
    [SerializeField] private ObstacleService obstacleService;

    private void Start()
    {
        spawner?.Initialize(SpawnHintForPlayer);
    }
    public void SpawnHintForPlayer(Mesh meshClone, int idPoint, IInteractable player)
    {
        HintBehaviour newHint = hint.Clone();
        Position position = positionController.GetPosition(idPoint);
        newHint.transform.position = new Vector3(position.transform.position.x, position.transform.position.y, obstacleService.GetCurentZPosition());
        player.DestroyPlayer += newHint.DestroyHint;
        newHint.OnPlayerChangedPosition(position.transform.position);
        player.PlayerChangedPosition += newHint.OnPlayerChangedPosition;
        newHint.OnPlayerChangedMesh(meshClone);
        player.PlayerChangedMesh += newHint.OnPlayerChangedMesh;
        obstacleService.ObtacleSwitch += newHint.OnObstacleChanged;
        newHint.SetObstacleService(obstacleService);
    }
}
