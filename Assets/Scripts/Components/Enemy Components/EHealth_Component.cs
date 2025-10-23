using Unity.VisualScripting;
using UnityEngine;

public class EHealth_Component : Entity_HealthComponent
{
    [Header("Details")]
    private float curHP;
    private float maxHP;

    public void SetMaxHP(int maxHP)
    {
        this.maxHP = maxHP;
    }

    public void DecreaseHP(float damage)
    {
        curHP -= damage;

        if (curHP <= 0)
            curHP = 0;
    }
}
