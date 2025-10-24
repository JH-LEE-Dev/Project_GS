using UnityEngine;

public class Gun_StatComponent : Entity_StatComponent
{
    // �ѱ� ���׷��̵� ��ġ, �ƽ�ġ
    public EnhanceValue upgradeAmount;

    private Player_StatComponent playerStats;

    public float GetTotalSpeed()
    {
        float baseSpeed = resource.baseSpeed.GetValue();
        float playerBonusSpeed = playerStats.upgradeAmount.speed.GetCalcValue();
        float gunBonusSpeed = upgradeAmount.speed.GetCalcValue();
        float finalSpeed = baseSpeed + playerBonusSpeed + gunBonusSpeed;

        return finalSpeed;
    }

    public override float GetTotalDamage(out bool isCrit, float scaleFactor = 1f)
    {
        float baseDamage = offense.baseDamage.GetValue();
        float playerBonusDamage = playerStats.upgradeAmount.damage.GetCalcValue();
        float gunBounusDamage = upgradeAmount.damage.GetCalcValue();
        float totalDamage = baseDamage + playerBonusDamage;
        // ���� ������� N% ����� �߰� 
        totalDamage += (totalDamage * (gunBounusDamage / 100f));

        float baseCritChance = offense.baseCritChance.GetValue();
        float playerCritChane = playerStats.upgradeAmount.critChance.GetCalcValue();
        float gunCritChance = upgradeAmount.critChance.GetCalcValue();
        float finalCritChance = baseCritChance + (playerCritChane / 100f) + (gunCritChance / 100f);
        finalCritChance = Mathf.Clamp(finalCritChance, 0f, 100f);

        isCrit = Random.Range(0f, 99f) < finalCritChance;

        float baseCritPower = offense.baseCritPower.GetValue();
        float bonusCritPower = playerStats.upgradeAmount.critPower.GetCalcValue();
        float gunBonusCritPower = upgradeAmount.critPower.GetCalcValue();
        float totalCritPower = (baseCritPower + bonusCritPower + gunBonusCritPower) / 100f;

        float finalDamage = isCrit ? totalDamage * totalCritPower : totalDamage;

        return finalDamage;
    }

    public void CashingPlayerStat(Player_StatComponent playerStats)
    {
        this.playerStats = playerStats;
    }
}
