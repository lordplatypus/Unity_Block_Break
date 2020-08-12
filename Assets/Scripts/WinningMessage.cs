using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinningMessage : MonoBehaviour
{
    Text text;

    void Start()
    {
        text = GetComponent<Text>();
    }
    public void DisplayWinningText()
    {
        if (Options.numColor < 3)
        {
            text.text = "Honestly do you think that you deserve that 'win'?";
        }
        else if (Options.numColor < 5)
        {
            text.text = "Not bad, think you can do the same with more colors?";
        }
        else if (Options.numColor == 5)
        {
            text.text = "Nice job, very impressive";
        }
        else
        {
            text.text = "You are a God among men, or you cheated";
        }
    }

    public void ResetText()
    {
        text.text = "";
    }
}
