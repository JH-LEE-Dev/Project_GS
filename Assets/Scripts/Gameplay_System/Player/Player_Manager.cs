using UnityEngine;

public class Player_Manager : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    private GameObject player;

    [Header("Spawn Details")]
    [SerializeField] private Transform spawnTransform;

    public GameObject SpawnPlayer()
    {
        if (playerPrefab == null || player != null)
            return default;

        player = Instantiate(playerPrefab);

        Player script = player.GetComponent<Player>();
        return script?.CreateGun(spawnTransform);
    }
}
