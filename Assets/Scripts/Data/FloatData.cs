using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Float", menuName = "Data/Float")]
public class FloatData : ScriptableObject
{
    public float value;

    //public static explicit operator bool(float data) { return data.value; }
}
