using UnityEngine;

public class Gun_StatComponent : Entity_StatComponent
{
    // �ѱ� ���׷��̵� ��ġ, �ƽ�ġ
    public EnhanceValue upgradeAmount;

    private Player_StatComponent playerStats;

    public float GetTotalSpeed()
    {
        float baseSpeed = resource.baseSpeed.GetValue();
        float playerSpeed = playerStats.upgradeAmount.speed.GetCalcValue();
        float gunBonusSpeed = upgradeAmount.speed.GetCalcValue();
        float finalSpeed = baseSpeed + playerSpeed + gunBonusSpeed;

        return finalSpeed;
    }

    public override float GetTotalDamage(out bool isCrit, float scaleFactor = 1f)
    {
        float baseDamage = offense.baseDamage.GetValue();
        float playerDamage = playerStats.upgradeAmount.damage.GetCalcValue();
        float gunBounusDamage = upgradeAmount.damage.GetCalcValue();
        float totalDamage = baseDamage + playerDamage;
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

    public int GetTotalPenCount()
    {
        float basePen = offense.basePen.GetValue();
        float playerPen = playerStats.upgradeAmount.pen.GetCalcValue();
        float gunBonusPen = upgradeAmount.pen.GetCalcValue();

        int finalPen = (int)(basePen + gunBonusPen + playerPen);

        return finalPen;
    }

    public float GetCalcFireSpeed()
    {
        // ���� �߻�ӵ� = ���� �⺻�ӵ� - ��ȭ �ӵ� = ( ��ȭ ��ġ�� �⺻�ӵ��� 25�۾� ���� ) 

        float baseFireSpeed = offense.fireSpeed.GetValue();
        int bonusCnt = upgradeAmount.fireSpeed.GetAmount();
        float bonusValue = upgradeAmount.fireSpeed.GetUpgradeValue();

        float finalFireSpeed = Mathf.Clamp(baseFireSpeed - bonusCnt * (baseFireSpeed * bonusValue), 0.15f, baseFireSpeed);

        return finalFireSpeed;
    }

    public void CashingPlayerStat(Player_StatComponent playerStats)
    {
        this.playerStats = playerStats;
    }
}
