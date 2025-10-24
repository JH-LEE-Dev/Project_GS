using System;
using UnityEngine;

[Serializable]
public class Stat_OffenseGroup
{
    public SimpleValue baseDamage;          // 공격력
    public SimpleValue baseCritChance;      // 크리티컬 확률
    public SimpleValue baseCritPower;       // 크리티컬 힘
    public SimpleValue baseAmmo;            // 탄약
    public SimpleValue basePen;             // 관통력
}
