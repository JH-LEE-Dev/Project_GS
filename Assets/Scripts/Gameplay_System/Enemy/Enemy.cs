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
}
