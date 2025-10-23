using System;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

[Serializable]
public class SimpleValue
{
    [SerializeField] private int value;

    public int GetValue() => value;
}
