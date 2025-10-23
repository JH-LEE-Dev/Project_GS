using System;
using UnityEngine;

public class Enemy : Entity
{
    [Header("Default Attribute")]
    

    [Header("Components")]
    private EMovement_Component moveComponent;
    private ECombat_Component combatComponent;
    private EHealth_Component healthComponent;
    private EStat_Component statComponent;

    [Header("Target Details")]
    private GameObject player;

    private void Awake()
    {
        moveComponent = GetComponent<EMovement_Component>();
        combatComponent = GetComponent<ECombat_Component>();   
        healthComponent = GetComponent<EHealth_Component>();
        statComponent = GetComponent<EStat_Component>();
    }

    private void Start()
    {
        moveComponent.SetSpeed(statComponent.GetSpeed());
    }

    public void SetPlayerRef(GameObject _player)
    {
        player = _player;
    }
}
