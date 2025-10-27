using System;
using Unity.VisualScripting;
using UnityEngine;

public class Wave_Manager : MonoBehaviour
{
    [Header("Player Details")]
    private GameObject player;
    [SerializeField] private float spawnDistanceAlpha;

    [Header("Spawn Details")]
    private GameObject enemyPrefab;

    public void OnEnable()
    {
        Game_Controller.OnStartWave += SpawnWave;    
    }

    public void OnDisable()
    {
        Game_Controller.OnStartWave -= SpawnWave;
    }

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

        int[] H = { 0, 0, 1, -1 };
        int[] W = { 1, -1, 0, 0 };

        for (int i = 0; i < 4; ++i)
        {
            Vector3 spawnPoint = playerPosition;
            spawnPoint.x += (W[i]*spawnDistanceAlpha) * cameraWidth;
            spawnPoint.y += (H[i]*spawnDistanceAlpha) * cameraHeight;

            Enemy_Manager.SpawnEnemy(spawnPoint,i);
        }
    }

    Tuple<float, float> GetCameraBoundary()
    {
        Camera cam = Camera.main;

        float height = cam.orthographicSize;

        float width = height * cam.aspect;

        var Ret = new Tuple<float, float>(height, width);

        return Ret;
    }
}
