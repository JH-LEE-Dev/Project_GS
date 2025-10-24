using UnityEngine;

public class Gun_StatComponent : Entity_StatComponent
{
    // �ѱ� ���׷��̵� ��ġ, �ƽ�ġ
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
