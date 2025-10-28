using Unity.VisualScripting;
using UnityEngine;

public class Enemy_Manager : MonoBehaviour
{
    [Header("Static Var for Spawning")]
    [SerializeField] EnemySpawnData enemySpawnData;
    private static GameObject player;
    private static GameObject[] enemyPrefabs;

    public static float reLocateDist;
    private static float reLocateMinRadius;
    private static float reLocateMaxRadius;

    private static int[] sideAlphaX = { 0, 0, 0, 0 };
    private static int[] sideAlphaY = { 0, 0, 0, 0 };

    private void Awake()
    {
        enemyPrefabs = enemySpawnData.enemyPrefabs;
        sideAlphaX = enemySpawnData.sideAlphaX;
        sideAlphaY = enemySpawnData.sideAlphaY;
        reLocateDist = enemySpawnData.reLocateDist;
        reLocateMinRadius = enemySpawnData.reLocateMinRadius;
        reLocateMaxRadius = enemySpawnData.reLocateMaxRadius;
    }

    public static void SpawnEnemy(Vector3 spawningTransform,int side)
    {
        for (int i = 0; i < 1; i++)
        {
            int spawnEnemyType = Random.Range((int)EnemyType.ZombieMen, (int)EnemyType.ZombieGirl2);
            Vector3 center = spawningTransform;

            float radius = Random.Range(0f, 1f);
            float angle = i * Mathf.PI * 2 / 50;
            float x = Mathf.Cos(angle) * (radius + sideAlphaX[side]) + center.x;
            float y = Mathf.Sin(angle) * (radius + sideAlphaY[side]) + center.y;
            Vector3 spawnPos = new Vector3(x, y, 0f);

            GameObject enemyObject = null;

            if (enemyPrefabs[spawnEnemyType] != null)
                enemyObject = Instantiate(enemyPrefabs[spawnEnemyType], spawnPos, Quaternion.identity);

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

    public static void ReLocateEnemy(GameObject enemy)
    {
        float radius = Random.Range(reLocateMinRadius, reLocateMaxRadius);
        float angle = Random.Range(0f, Mathf.PI * 2);
        float x = Mathf.Cos(angle) * (radius) + player.transform.position.x;
        float y = Mathf.Sin(angle) * (radius) + player.transform.position.y;

        Vector3 spawnPos = new Vector3(x, y, 0f);

        enemy.transform.position = spawnPos;
    }
}
