using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class EStat_Component : Entity_StatComponent
{
    public float GetSpeed()
    {
        float BaseSpeed = GetBaseSpeed();

        float Alpha = Random.Range(0f, 1f);

        return BaseSpeed + Alpha;
    }
}
