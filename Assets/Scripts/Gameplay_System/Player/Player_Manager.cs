using UnityEngine;

public class Player_Manager : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    private GameObject player;

    [Header("Spawn Details")]
    [SerializeField] private Transform spawnTransform;

    public GameObject SpawnPlayer()
    {
        if(playerPrefab != null && player == null)
        {
            player = Instantiate(playerPrefab, spawnTransform.position, Quaternion.identity);
        }

        return player;
    }
}
