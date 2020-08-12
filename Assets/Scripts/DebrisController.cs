using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebrisController : MonoBehaviour
{
    System.Random rand;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rand = new System.Random();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<Transform>().transform.position.y < -2 || 
            GetComponent<Transform>().transform.position.x < -3 ||
            GetComponent<Transform>().transform.position.x > 13)
        {
            Kill();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == gameObject.tag)
        {
            if (other.transform.position.x > GetComponent<Transform>().transform.position.x)
            {
                rb.AddForce(new Vector2(rand.Next(-50, -10), rand.Next(-10, 10)));
            }
            else
            {
                rb.AddForce(new Vector2(rand.Next(10, 50), rand.Next(-10, 10)));
            }
        }
    }

    void Kill()
    {
        Destroy(gameObject);
    }
}
