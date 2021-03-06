using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "eForce", menuName = "Data/Enum/eForce")]
public class ForceModeData : EnumData
{
    public enum eType
    {
        Constant,
        InverseLinear,
        InverseSquared
    }

    public eType value;

    public override int index { get => (int)value; set => this.value = (eType)value; }
    public override string[] names { get => Enum.GetNames(typeof(eType)); }
}
