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
    public UpgradeValues health;          // ü��
    public UpgradeValues armour;          // ���
    public UpgradeValues speed;           // �̵��ӵ�
    public UpgradeValues ammo;            // ź��
    public UpgradeValues damage;          // �����
    public UpgradeValues pen;             // ����
    public UpgradeValues accuracy;        // ���߷�
    public UpgradeValues launchSpeed;     // �߻�ӵ�
    public UpgradeValues reloadSpeed;     // �������ӵ�
    public UpgradeValues critChance;      // ũ��Ƽ�� Ȯ��
    public UpgradeValues critPower;      // ũ��Ƽ�� �����
}

