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
    public GameObject SpawnPlayer(bool isGunSpawn = false)
    {
        if (playerPrefab == null || player != null)
            return default;

        player = Instantiate(playerPrefab, spawnTransform.position, Quaternion.identity);

        if (isGunSpawn)
        {
            Player script = player.GetComponent<Player>();
            script?.CreateGun(player.transform);
        }

        return player;
    }
}
