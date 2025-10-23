using System;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

[Serializable]
public class SimpleValue
{
    [SerializeField] private float value;

    public float GetValue() => value;
}
