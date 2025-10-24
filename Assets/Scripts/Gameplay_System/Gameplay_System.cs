using UnityEngine;

public class Gameplay_System : MonoBehaviour
{
    private Player_Manager playerManager;
    private Enemy_Manager enemyManager;
    private Wave_Manager waveManager;

    private void Awake()
    {
        playerManager = GetComponent<Player_Manager>();
        enemyManager = GetComponent<Enemy_Manager>();
        waveManager = GetComponent<Wave_Manager>();
    }

    private void Start()
    {
        GameObject curGun = playerManager.SpawnPlayer();

        if (curGun == null)
            return;

        enemyManager.Initialize(curGun);
        waveManager.Initialize(curGun);

        waveManager.StartWave();    
    }
}
