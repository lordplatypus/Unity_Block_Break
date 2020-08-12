using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsHeight : MonoBehaviour
{
    Text text;
    void Start()
    {
        text = GetComponent<Text>();
        text.text = Options.height.ToString();
    }
    public void UpdateHeightMinus()
    {
        if (Options.height - 1 != 0) Options.height -= 1;
        text.text = Options.height.ToString();
    }
    public void UpdateHeightPlus()
    {
        if (Options.height + 1 != 21) Options.height += 1;
        text.text = Options.height.ToString();
    }
}
