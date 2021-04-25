using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelSelect : MonoBehaviour
{
    public GameObject[] panels;
    int selected = 0;

    KeyCode decrementCode = KeyCode.LeftArrow;
    KeyCode incrementCode = KeyCode.RightArrow;

    private void Update()
    {
        if (Input.GetKeyDown(decrementCode)) Decrement();
        if (Input.GetKeyDown(incrementCode)) Increment();

        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].SetActive(i == selected);
        }
    }

    public void Increment()
    {
        selected++;
        if (selected >= panels.Length) selected = 0;
    }

    public void Decrement()
    {
        selected--;
        if (selected <= -1) selected = panels.Length - 1;
    }
}
