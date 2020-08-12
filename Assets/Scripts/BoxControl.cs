using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxControl : MonoBehaviour
{
    DebrisReference debris;
    ObjectReference box;
    ParticleManager particleManager;
    Vector2 position;
    public bool isSelected = false;
    // Start is called before the first frame update
    void Start()
    {
        debris = GameObject.Find("Game").GetComponent<DebrisReference>();
        box = GameObject.Find("Game").GetComponent<ObjectReference>();
        particleManager = GameObject.Find("Game").GetComponent<ParticleManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IsSelected()
    {
        if (!isSelected) isSelected = true;
        else isSelected = false;
    }

    public void Break()
    {
        position = GetComponent<Transform>().transform.position;
        GameObject boxDebrisType = null;
        if (gameObject.tag == box.Blue.tag) boxDebrisType = debris.BlueDebris;
        if (gameObject.tag == box.Cyan.tag) boxDebrisType = debris.CyanDebris;
        if (gameObject.tag == box.Green.tag) boxDebrisType = debris.GreenDebris;
        if (gameObject.tag == box.Orange.tag) boxDebrisType = debris.OrangeDebris;
        if (gameObject.tag == box.Purple.tag) boxDebrisType = debris.PurpleDebris;
        if (gameObject.tag == box.Red.tag) boxDebrisType = debris.RedDebris;
        if (gameObject.tag == box.White.tag) boxDebrisType = debris.WhiteDebris;
        if (gameObject.tag == box.Yellow.tag) boxDebrisType = debris.YellowDebris;
              
        for (float x = 0; x < 1; x += .5f)
        {
            for (float y = 0; y < 1; y += .5f)
            {
                particleManager.Debris(new Vector3(x + position.x, y + position.y, 0), boxDebrisType);
            }
        }
        GameObject.Find("Game").GetComponent<ParticleManager>().Glitter(position);
        
        Kill();
    }

    void Kill()
    {
        Destroy(gameObject);
    }
}
