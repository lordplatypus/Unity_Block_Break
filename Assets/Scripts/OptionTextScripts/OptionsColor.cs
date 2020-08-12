using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsColor : MonoBehaviour
{
    Text text;
    void Start()
    {
        text = GetComponent<Text>();
        text.text = Options.numColor.ToString();
    }
    public void UpdateColorMinus()
    {
        if (Options.numColor - 1 != 0) Options.numColor -= 1;
        text.text = Options.numColor.ToString();
    }
    public void UpdateColorPlus()
    {
        if (Options.numColor + 1 != 9) Options.numColor += 1;
        text.text = Options.numColor.ToString();
    }
}
