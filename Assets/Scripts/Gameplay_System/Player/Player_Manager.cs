using UnityEngine;

public class Player_Manager : MonoBehaviour
{
    /// <summary>
    /// Attribute
    /// </summary>
    private GameObject player;

    [Header("Spawn Details")]
    [SerializeField] private Transform spawnTransform;

    /// <summary>
    /// Functions
    /// </summary>
    public void SpawnPlayer()
    {
        if(player == null)
        {
            player = Instantiate(player, spawnTransform.position, Quaternion.identity);
        }
    }

    private void Start()
    {
        SpawnPlayer();
    }
}
