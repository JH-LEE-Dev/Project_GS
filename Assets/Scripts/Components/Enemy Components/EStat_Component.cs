using System;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;

public class EStat_Component : Entity_StatComponent
{
    public Tuple<float,float> GetSpeed()
    {
        float BaseSpeed = resource.baseSpeed.GetValue();

        float Alpha = UnityEngine.Random.Range(0f, 1f);

        Tuple<float, float> Ret = new Tuple<float, float>(BaseSpeed+Alpha, Alpha);

        return Ret;
    }
}
