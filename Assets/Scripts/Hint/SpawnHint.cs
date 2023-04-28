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
        newHint.OnPlayerChangedPosition(position.transform.position);
        newHint.OnPlayerChangedMesh(meshClone);
        newHint.SetObstacleService(obstacleService);
        InitializeEvents(player, newHint);
    }
    private void InitializeEvents(IInteractable player, HintBehaviour newHint)
    {
        player.PlayerChangedMesh += newHint.OnPlayerChangedMesh;
        obstacleService.ObtacleSwitch += newHint.OnObstacleChanged;
        player.PlayerChangedPosition += newHint.OnPlayerChangedPosition;
        player.DestroyPlayer += newHint.DestroyHint;
        player.СolliderWithHintTrigger += newHint.StartAnimation;
        player.PlayerPause += newHint.PauseAnimation;
    }
}
