using UnityEngine;

public class Gun_StatComponent : Entity_StatComponent
{
    // 총기 업그레이드 수치, 맥스치
    public EnhanceValue upgradeAmount;

    private Player_StatComponent playerStats;

    public virtual float GetTotalDamage(out bool isCrit, float scaleFactor = 1f)
    {
        float baseDamage = offense.baseDamage.GetValue();
        float playerBonusDamage = playerStats.upgradeAmount.damage.GetCalcValue();
        float gunBounusDamage = upgradeAmount.damage.GetCalcValue();
        float calcDamage = baseDamage + playerBonusDamage;

        isCrit = false;
        return 0f;
    }
}
