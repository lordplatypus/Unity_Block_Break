using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetLimit : MonoBehaviour
{
    Button reset;
    int coolDown;
    bool activateCoolDown;
    // Start is called before the first frame update
    void Start()
    {
        reset = GetComponent<Button>();
        coolDown = 90;
        activateCoolDown = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (activateCoolDown)
        {
            reset.interactable = false;
            coolDown--;
            if (coolDown == 0)
            {
                coolDown = 90;
                reset.interactable = true;
                activateCoolDown = false;
            }
        }
    }

    public void ActivateCoolDown()
    {
        activateCoolDown = true;
    }
}
