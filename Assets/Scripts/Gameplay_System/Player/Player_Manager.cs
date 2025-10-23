using UnityEngine;

public class Player_Manager : MonoBehaviour
{
    /// <summary>
    /// Attribute
    /// </summary>
    private GameObject playerPrefab;
    private GameObject player;

    [Header("Spawn Details")]
    [SerializeField] private Transform spawnTransform;

    /// <summary>
    /// Functions
    /// </summary>
    public GameObject SpawnPlayer()
    {
        if(playerPrefab != null && player == null)
        {
            player = Instantiate(playerPrefab, spawnTransform.position, Quaternion.identity);
        }

        return player;
    }
}
