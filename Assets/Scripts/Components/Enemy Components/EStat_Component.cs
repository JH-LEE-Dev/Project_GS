using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class EStat_Component : Entity_StatComponent
{
    [Header("Stat Details")]
    [SerializeField] private int damage;
    [SerializeField] private int maxHP;
    [SerializeField] private float speed;

    public float GetSpeed()
    {
        return speed;
    }

    public int GetDamage()
    {
        return damage;
    }

    public int GetMaxHP()
    {
        return maxHP;
    }
}
