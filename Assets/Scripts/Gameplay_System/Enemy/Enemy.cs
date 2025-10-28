using System;
using UnityEngine;

public class Enemy : Entity
{
    [Header("Default Attribute")]
    private EnemyType type;

    [Header("Components")]
    private EMovement_Component movementComponent;
    private ECombat_Component combatComponent;
    private EHealth_Component healthComponent;
    private EStat_Component statComponent;
    private Enemy_StateController stateController;

    [Header("Target Details")]
    private GameObject player;

    private void Awake()
    {
        movementComponent = GetComponent<EMovement_Component>();
        combatComponent = GetComponent<ECombat_Component>();   
        healthComponent = GetComponent<EHealth_Component>();
        statComponent = GetComponent<EStat_Component>();
        stateController = GetComponent<Enemy_StateController>();

        healthComponent.SetHPData(statComponent.resource.baseHealth.GetValue());
    }

    private void OnEnable()
    {
        healthComponent.OnEnemyDead += HandleEnemyDead;
    }

    private void OnDisable()
    {
        healthComponent.OnEnemyDead -= HandleEnemyDead;
    }

    private void Start()
    {
        Tuple<float, float> speedData = statComponent.GetSpeed();

        movementComponent.SetSpeed(speedData.Item1);
        stateController.SetMoveSpeedMultiplier(speedData.Item2);
    }

    public void SetPlayerRef(GameObject _player)
    {
        player = _player;
        movementComponent.SetTarget(player);
    }

    private void Update()
    {
        HandleRelocate();
    }

    private void HandleRelocate()
    {
        var posDelta = player.transform.position - transform.position;

        if (posDelta.magnitude > Enemy_Manager.reLocateDist)
        {
            Enemy_Manager.ReLocateEnemy(gameObject);
        }
    }

    private void HandleEnemyDead()
    {
        Destroy(gameObject);
    }
}
