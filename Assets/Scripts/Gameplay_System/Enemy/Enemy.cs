using System;
using UnityEngine;

public class Enemy : Entity
{
    [Header("Components")]
    private EMove_Component moveComponent;
    private ECombat_Component combatComponent;
    private EHealth_Component healthComponent;
    private EStat_Component statComponent;

    [Header("Target Details")]
    private GameObject target;

    private void Awake()
    {
        moveComponent = GetComponent<EMove_Component>();
        combatComponent = GetComponent<ECombat_Component>();   
        healthComponent = GetComponent<EHealth_Component>();
        statComponent = GetComponent<EStat_Component>();
    }

    private void Start()
    {
        moveComponent.SetSpeed(statComponent.GetSpeed());
    }

    public void SetTarget(GameObject player)
    {
        target = player;
    }
}
