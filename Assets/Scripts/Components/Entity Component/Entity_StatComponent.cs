using UnityEngine;

public class Entity_StatComponent : MonoBehaviour
{
    [SerializeField] protected Stat_OffenceGroup offence;
    [SerializeField] protected Stat_ResourceGroup resource;

    public float GetTotalDamage(out bool isCrit, float scaleFactor = 1f)
    {
        float baseDamage = GetBaseDamage();
        float baseCritChance = GetBaseCritChance();
        float baseCritPower = 100f + GetBaseCritPower();
        float totalCritPower = baseCritPower / 100f;

        isCrit = Random.Range(0f, 100f) < baseCritChance;
        float finalDamage = isCrit ? baseDamage : baseDamage * totalCritPower;

        return finalDamage * scaleFactor;
    }

    public virtual float GetBaseDamage() => offence.baseDamage.GetValue();
    public virtual float GetBaseCritChance() => offence.baseCritChance.GetValue();
    public virtual float GetBaseCritPower() => offence.baseCritPower.GetValue();
    public virtual float GetBaseAmmo() => offence.baseAmmo.GetValue();
    public virtual float GetBasePen() => offence.basePen.GetValue();
    public virtual float GetBaseHealth() => resource.baseHealth.GetValue();
    public virtual float GetBaseArmour() => resource.baseArmour.GetValue();
    public virtual float GetBaseSpeed() => resource.baseSpeed.GetValue();
}
