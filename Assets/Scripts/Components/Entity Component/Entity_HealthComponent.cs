using UnityEngine;

public class Entity_HealthComponent : MonoBehaviour, IDamageable
{
    /*
     * �Ӽ�
     */
    [Header("Details")]
    private float curHP;
    private float maxHP;

    /*
     * �Լ� 
     */
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

    public virtual void TakeDamage(float damage)
    {
        
    }
}
