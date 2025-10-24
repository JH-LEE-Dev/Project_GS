using UnityEngine;

[System.Serializable]
public class UpgradeValues
{
    [SerializeField] private int curUpgradeAmount;
    [SerializeField] private int maxUgradeAmount;

    [SerializeField] private float upgradeValue;

    public int GetAmount() => curUpgradeAmount;

    public float GetCalcValue()
    {
        return curUpgradeAmount * upgradeValue;
    }

    public bool SetAmount(int amount)
    {
        int newAmount = curUpgradeAmount + amount;

        if (newAmount > maxUgradeAmount)
            return false;

        curUpgradeAmount = Mathf.Max(newAmount, 0);
        return true;
    }
}

[System.Serializable]
public class EnhanceValue
{
    public UpgradeValues health;          // 체력
    public UpgradeValues armour;          // 방어
    public UpgradeValues speed;           // 이동속도
    public UpgradeValues ammo;            // 탄약
    public UpgradeValues damage;          // 대미지
    public UpgradeValues pen;             // 관통
    public UpgradeValues accuracy;        // 명중률
    public UpgradeValues launchSpeed;     // 발사속도
    public UpgradeValues reloadSpeed;     // 재장전속도
    public UpgradeValues critChance;      // 크리티컬 확률
    public UpgradeValues critPower;      // 크리티컬 대미지
}

