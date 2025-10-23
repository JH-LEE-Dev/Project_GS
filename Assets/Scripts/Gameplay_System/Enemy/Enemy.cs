using UnityEngine;

public class Enemy : Entity
{
    [Header("Components")]
    private EMove_Component moveComponent;
    private ECombat_Component combatComponent;
    private EHealth_Component healthComponent;

    [Header("Target Details")]
    private GameObject target;

    private void Awake()
    {
        moveComponent = GetComponent<EMove_Component>();
        combatComponent = GetComponent<ECombat_Component>();   
        healthComponent = GetComponent<EHealth_Component>();
    }

    public void SetTarget(GameObject player)
    {
        target = player;
    }
}
