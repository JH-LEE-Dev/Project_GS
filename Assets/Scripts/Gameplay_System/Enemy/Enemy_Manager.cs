using Unity.VisualScripting;
using UnityEngine;

public class Enemy_Manager : MonoBehaviour
{
    [Header("Static Var for Spawning")]
    [SerializeField] EnemySpawnData enemySpawnData;
    private static GameObject player;
    private static GameObject enemyPrefab;

    private static int[] sideAlphaX = { 0, 0, 0, 0 };
    private static int[] sideAlphaY = { 0, 0, 0, 0 };

    private void Awake()
    {
        enemyPrefab = enemySpawnData.enemyPrefab;
        sideAlphaX = enemySpawnData.sideAlphaX;
        sideAlphaY = enemySpawnData.sideAlphaY;
    }

    public static void SpawnEnemy(Vector3 spawningTransform,int side)
    {
        for (int i = 0; i < 50; i++)
        {
            Vector3 center = spawningTransform;

            float radius = Random.Range(0f, 1f);
            float angle = i * Mathf.PI * 2 / 50;
            float x = Mathf.Cos(angle) * (radius + sideAlphaX[side]) + center.x;
            float y = Mathf.Sin(angle) * (radius + sideAlphaY[side]) + center.y;
            Vector3 spawnPos = new Vector3(x, y, 0f);

            GameObject enemyObject = null;

            if (enemyPrefab != null)
                enemyObject = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);

            if (enemyObject != null)
            {
                Enemy enemy = enemyObject.GetComponent<Enemy>();

                if (player != null && enemy)
                {
                    enemy.SetPlayerRef(player);
                }
            }
        }
    }

    public void Initialize(GameObject _player)
    {
        player = _player;
    }
}
