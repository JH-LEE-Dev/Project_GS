using Unity.VisualScripting;
using UnityEngine;

public class Enemy_Manager : MonoBehaviour
{
    [Header("Static Var for Spawning")]
    [SerializeField] EnemySpawnData enemySpawnData;
    private static GameObject player;
    private static GameObject enemyPrefab;

    private void Awake()
    {
        enemyPrefab = enemySpawnData.enemyPrefab;
    }

    public static void SpawnEnemy(Vector3 spawningTransform)
    {
        for (int i = 0; i < 25; i++)
        {
            GameObject enemyObject = null;

            if (enemyPrefab != null)
                enemyObject = Instantiate(enemyPrefab, spawningTransform, Quaternion.identity);

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
