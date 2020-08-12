using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionController : MonoBehaviour
{
    Animator anim;
    SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.Play("Selection");
    }

    public void IsVisible()
    {
        if (sr.enabled) sr.enabled = false;
        else sr.enabled = true;
    }

    public void Kill()
    {
        Destroy(gameObject);
    }
}
