using UnityEngine;

public class Entity_StatComponent : MonoBehaviour
{
    public Stat_OffenseGroup offense;
    public Stat_ResourceGroup resource;

    public virtual float GetTotalDamage(out bool isCrit, float scaleFactor = 1f)
    {
        float baseDamage = offense.baseDamage.GetValue();
        float baseCritChance = offense.baseCritChance.GetValue();
        float baseCritPower = 100f + offense.baseCritPower.GetValue();
        float totalCritPower = baseCritPower / 100f;

        isCrit = Random.Range(0f, 100f) < baseCritChance;
        float finalDamage = isCrit ? baseDamage : baseDamage * totalCritPower;

        return finalDamage * scaleFactor;
    }
}
