using UnityEngine;

[CreateAssetMenu(fileName = "EnemySpawnData", menuName = "Game/Enemy Database", order = 0)]
public class EnemySpawnData : ScriptableObject
{
    [Header("Prefab References")]
    public GameObject enemyPrefab;      
    public GameObject bossPrefab;       
}