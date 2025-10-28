using System;
using UnityEngine;

namespace Game.Stats
{
    [System.Serializable]
    public class SimpleValue
    {
        [SerializeField] private float value;

        public float GetValue() => value;
    }


    [System.Serializable]
    public class Stat_ResourceGroup
    {
        public SimpleValue baseHealth;          // ü��
        public SimpleValue baseArmour;          // ���
        public SimpleValue baseSpeed;           // �̵��ӵ�
    }

    [System.Serializable]
    public class Stat_OffenseGroup
    {
        public SimpleValue baseDamage;          // ���ݷ�
        public SimpleValue baseCritChance;      // ũ��Ƽ�� Ȯ��
        public SimpleValue baseCritPower;       // ũ��Ƽ�� ��
        public SimpleValue baseAmmo;            // ź��
        public SimpleValue basePen;             // �����
        public SimpleValue fireSpeed;           // �߻� �ӵ�
    }
}

namespace Game.Prefab
{
    [System.Serializable]
    public class GunPrefab
    {
        public GunName name;
        public GameObject prefab;
    }
}