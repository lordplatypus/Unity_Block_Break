using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsWidth : MonoBehaviour
{
    Text text;
    void Start()
    {
        text = GetComponent<Text>();
        text.text = Options.width.ToString();
    }
    public void UpdateWidthMinus()
    {
        if (Options.width - 1 != 0) Options.width -= 1;
        text.text = Options.width.ToString();
    }

    public void UpdateWidthPlus()
    {
        if (Options.width + 1 != 11) Options.width += 1;
        text.text = Options.width.ToString();
    }
}
