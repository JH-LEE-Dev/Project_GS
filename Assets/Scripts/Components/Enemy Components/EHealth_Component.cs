using System;
using Unity.VisualScripting;
using UnityEngine;

public class EHealth_Component : Entity_HealthComponent, IDamageable
{
    public event Action OnEnemyDead;

    [Header("Attributes")]
    private float curHP;
    private float maxHP;
    private bool bDead = false;

    public void SetHPData(float _maxHP)
    {
        maxHP = _maxHP;
        curHP = maxHP;
    }

    public bool IsEnemyDead()
    {
        return bDead;
    }

    public void SetEnemyDead(bool _boolean)
    {
        bDead = _boolean;
    }

    public override void TakeDamage(float damage)
    {
        curHP -= damage;

        if (curHP < 0)
        {
            curHP = 0;
            bDead = true;
            OnEnemyDead.Invoke();
        }
    }
}
