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

    public Editor editor;

    private void Update()
    {
        if (Input.GetKeyDown(decrementCode)) Decrement();
        if (Input.GetKeyDown(incrementCode)) Increment();

        for (int i = 0; i < panels.Length; i++)
        {
            bool active = i == selected;
            panels[i].SetActive(active);

            if (active)
            {
                ActionHolder ah = panels[i].GetComponent<ActionHolder>();
                editor.SetAction(ah.actionType);
            }
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
