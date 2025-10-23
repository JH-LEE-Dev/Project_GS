using Unity.VisualScripting;
using UnityEngine;

public class Enemy_Manager : MonoBehaviour
{
    private GameObject enemyPrefab;
    private Transform spawnTransform;
    private GameObject player;

    void SpawnEnemy()
    {
        GameObject enemyObject = null;

        if (enemyPrefab != null)
            enemyObject = Instantiate(enemyPrefab, spawnTransform.position, Quaternion.identity);

        if (enemyObject != null)
        {
            Enemy enemy = enemyObject.GetComponent<Enemy>();

            if (player != null)
                enemy.SetPlayerRef(player);
        }
    }

    public void SetPlayerRef(GameObject _player)
    {
        player = _player;
    }
}
