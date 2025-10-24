using System;
using Unity.VisualScripting;
using UnityEngine;

public class Wave_Manager : MonoBehaviour
{
    [Header("Player Details")]
    private GameObject player;

    [Header("Spawn Details")]
    private GameObject enemyPrefab;

    public void Initialize(GameObject _player)
    {
        player = _player;
    }

    private void SpawnWave()
    {
        Tuple<float, float> cameraBoundary = GetCameraBoundary();
        float cameraHeight = cameraBoundary.Item1;
        float cameraWidth = cameraBoundary.Item2;

        Vector3 playerPosition = player.transform.position;
        playerPosition.x += cameraHeight;
        playerPosition.y += cameraWidth;

        Debug.Log("playerPositionx : " + playerPosition.x);
        Debug.Log("playerPositiony : " + playerPosition.y);

        Enemy_Manager.SpawnEnemy(playerPosition);
    }

    public void StartWave()
    {
        SpawnWave();
    }

    Tuple<float ,float> GetCameraBoundary()
    {
        Camera cam = Camera.main;

        float height = cam.orthographicSize * 2f;

        float width = height * cam.aspect;

        var Ret  = new Tuple<float, float>(height,width);

        return Ret;
    }
}
