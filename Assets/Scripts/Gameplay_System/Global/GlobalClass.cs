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
        public SimpleValue baseHealth;          // 체력
        public SimpleValue baseArmour;          // 방어
        public SimpleValue baseSpeed;           // 이동속도
    }

    [System.Serializable]
    public class Stat_OffenseGroup
    {
        public SimpleValue baseDamage;          // 공격력
        public SimpleValue baseCritChance;      // 크리티컬 확률
        public SimpleValue baseCritPower;       // 크리티컬 힘
        public SimpleValue baseAmmo;            // 탄약
        public SimpleValue basePen;             // 관통력
        public SimpleValue fireSpeed;           // 발사 속도
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