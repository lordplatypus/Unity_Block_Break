using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsNumHeight : MonoBehaviour
{
    Text text;
    Text num;
    Vector3 mousePosition;
    bool fade = false;
    int fadeOut = 120;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        num = GameObject.Find("Box Count Text").GetComponent<Text>();
        text.text = Options.boxCountNumHeight.ToString();
        num.enabled = false;
    }

    void Update()
    {
        if (fade)
        {
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Camera.main.ScreenToWorldPoint(GameObject.Find("Box Count Text").GetComponent<RectTransform>().position);
            GameObject.Find("Box Count Text").GetComponent<RectTransform>().position = mousePosition + new Vector3(0, Options.boxCountNumHeight, 50);
            fadeOut--;
            if (fadeOut < 60)
            {
                float opacity = fadeOut / 60;
                num.CrossFadeAlpha(opacity, 1, true);
                Debug.Log(fadeOut);
            }
            if (fadeOut == 0)
            {
                num.enabled = false;
                fade = false;
                fadeOut = 120;
            }
        }
    }

    public void UpdateNumHeightMinus()
    {
        if (Options.boxCountNumHeight - 1 == 0) Options.boxCountNumHeight -= 2;
        else if (Options.boxCountNumHeight - 1 != -6) Options.boxCountNumHeight -= 1;
        text.text = Options.boxCountNumHeight.ToString();
        num.enabled = true;
        num.CrossFadeAlpha(1, 0, true);
        fade = true;
        fadeOut = 120;
    }
    public void UpdateNumHeightPlus()
    {
        if (Options.boxCountNumHeight + 1 == 0) Options.boxCountNumHeight += 2;
        else if (Options.boxCountNumHeight + 1 != 6) Options.boxCountNumHeight += 1;
        text.text = Options.boxCountNumHeight.ToString();
        num.enabled = true;
        num.CrossFadeAlpha(1, 0, true);
        fade = true;
        fadeOut = 120;
    }
}
