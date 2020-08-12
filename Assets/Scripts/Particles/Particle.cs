using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    private Transform t;
    private SpriteRenderer sr;

    public bool isDead = false;
    public float x;
    public float y;
    public int lifeSpan;
    public float vx;
    public float vy;
    public float forceX;
    public float forceY;
    public float startScale = 1f;
    public float endScale = 1f;
    public float red = 1;
    public float green = 1;
    public float blue = 1;
    public int startAlpha = 1;
    public int endAlpha = 0;
    public float angle;
    public float angularVelocity;
    public float angularDamp = 1f;
    public float damp = 1f;
    private int age = 0;
    private float scale;
    private Color alpha;
    // Start is called before the first frame update
    void Start()
    {
        t = GetComponent<Transform>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        age++;

        if (age > lifeSpan)
        {//kill particle once its age hits its lifespan
            Destroy(gameObject);
            return;
        }

        float progressRate = (float)age / lifeSpan;

        //changes the scale of the particle
        scale = MyMath.Lerp(startScale, endScale, progressRate);
        t.localScale = new Vector3(scale, scale, 0);

        //force the particle in a certain direction
        vx += forceX;
        vy += forceY;

        //slows down particle (damp) (.01~.99)
        vx *= damp;
        vy *= damp;

        //actually updates particle position
        x += vx;
        y += vy;
        t.position = new Vector3(x, y, 0);

        //angle stuff
        angularVelocity *= angularDamp;
        angle += angularVelocity;       

        //color and alpha of the particle
        alpha = new Color(red, green, blue, MyMath.Lerp(startAlpha, endAlpha, progressRate));
        sr.color = alpha;
    }
}
