using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "BodyPhaseType", menuName = "Data/Enum/Broadphase")]
public class BroadPhaseType : EnumData
{
    public enum eType
    {
        None,
        QuadTree,
        BVH
    }

    public eType value;

    public override int index { get => (int)value; set => this.value = (eType)value; }
    public override string[] names { get => Enum.GetNames(typeof(eType)); }
}

