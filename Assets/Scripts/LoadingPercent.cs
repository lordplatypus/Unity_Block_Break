using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingPercent : MonoBehaviour
{
    Text text;
    LoadGameScene load;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        load = GameObject.Find("Loading").GetComponent<LoadGameScene>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(load.LoadingProgress());
        text.text = load.LoadingProgress().ToString();
    }
}
