using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class FloatUI : MonoBehaviour
{
    public Slider slider = null;
    public Text label = null;
    public Text Value = null;

    public FloatData data = null;

    private void OnValidate()
    {
        if (data != null)
        {
            name = data.name;
            label.text = name;
        }
    }

    private void Start()
    {
        slider.onValueChanged.AddListener(UpdateValue);
    }

    void Update()
    {
        Value.text = data.value.ToString();
    }
    void UpdateValue(float value)
    {
        data.value = slider.value;
    }
}