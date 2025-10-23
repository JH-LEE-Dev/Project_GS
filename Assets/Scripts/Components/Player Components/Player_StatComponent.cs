using UnityEngine;

public class Player_StatComponent : Entity_StatComponent
{
    // 플레이어가 보관(구매)한 스탯 보유량
    [SerializeField] private Stat_OffenceGroup curOffence;
    [SerializeField] private Stat_ResourceGroup curResource;

    public override float GetBaseDamage() => curOffence.baseDamage.GetValue();
    public override float GetBaseCritChance() => curOffence.baseCritChance.GetValue();
    public override float GetBaseCritPower() => curOffence.baseCritPower.GetValue();
    public override float GetBaseAmmo() => curOffence.baseAmmo.GetValue();
    public override float GetBasePen() => curOffence.basePen.GetValue();
    public override float GetBaseHealth() => curResource.baseHealth.GetValue();
    public override float GetBaseArmour() => curResource.baseArmour.GetValue();
    public override float GetBaseSpeed() => curResource.baseSpeed.GetValue();
}
