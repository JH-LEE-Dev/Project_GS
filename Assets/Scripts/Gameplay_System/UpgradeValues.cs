using UnityEngine;

[System.Serializable]
public class UpgradeValues
{
    private int curUpgradeAmount;
    private int maxUgradeAmount;

    private float upgradeValue;

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
    public UpgradeValues health;          // ü��
    public UpgradeValues armour;          // ���
    public UpgradeValues speed;           // �̵��ӵ�
    public UpgradeValues ammo;            // ź��
    public UpgradeValues damage;          // �����
    public UpgradeValues pen;             // ����
    public UpgradeValues accuracy;        // ���߷�
    public UpgradeValues launchSpeed;     // �߻�ӵ�
    public UpgradeValues reloadSpeed;     // �������ӵ�
    public UpgradeValues CritChance;      // ũ��Ƽ�� Ȯ��
    public UpgradeValues CritDamage;      // ũ��Ƽ�� �����
}

