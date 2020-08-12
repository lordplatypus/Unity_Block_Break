using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    GameObject particle;
    Particle p;
    ParticleReference pRef;
    // Start is called before the first frame update
    void Start()
    {
        MyMath.Init();
        pRef = GetComponent<ParticleReference>();
    }

    public void Debris(Vector3 position, GameObject boxDebrisType)
    {
        particle = Instantiate(boxDebrisType, new Vector3Int(0, -1, 0), Quaternion.identity);
        p = particle.GetComponent<Particle>();

        p.x = position.x;
        p.y = position.y;
        p.lifeSpan = MyMath.Range(80, 100);
        p.vx = MyMath.Range(-.1f, .1f);
        p.vy = MyMath.Range(-.3f, -.1f);
        p.startScale = 1;
        p.endScale = MyMath.Range(0, .1f);
        p.damp = .95f;
    }

    public void Star(Vector3 position)
    {
        particle = Instantiate(pRef.Star, new Vector3Int(0, -1, 0), Quaternion.identity);
        p = particle.GetComponent<Particle>();

        p.x = position.x + MyMath.Range(-.3f, .3f);
        p.y = position.y + MyMath.Range(-.3f, .3f);
        p.lifeSpan = MyMath.Range(10, 20);
        p.vx = 0;
        p.vy = MyMath.Range(-.1f, 0f);
        p.startScale = MyMath.Range(.3f, .4f);
        p.endScale = MyMath.Range(0, .1f);
        p.red = MyMath.Range(.1f, 1f);
        p.green = MyMath.Range(.1f, 1f);
        p.blue = MyMath.Range(.1f, 1f);
        p.damp = .97f;
    }

    public void Slash(Vector3 position)
    {
        particle = Instantiate(pRef.Slash2, new Vector3Int(0, -1, 0), Quaternion.identity);
        p = particle.GetComponent<Particle>();

        p.x = position.x;
        p.y = position.y;
        p.lifeSpan = 20;
        p.startScale = 1;
        p.endScale = 1;
        p.damp = .97f;
    }

    public void SlashV(Vector3 position)
    {
        particle = Instantiate(pRef.Slash2V, new Vector3Int(0, -1, 0), Quaternion.identity);
        p = particle.GetComponent<Particle>();

        p.x = position.x;
        p.y = position.y;
        p.lifeSpan = 20;
        p.startScale = 1;
        p.endScale = 1;
        p.damp = .97f;
    }

    public void Glitter(Vector3 position)
    {
        particle = Instantiate(pRef.Glitter, new Vector3Int(0, -1, 0), Quaternion.identity);
        p = particle.GetComponent<Particle>();

        p.x = position.x + .5f;
        p.y = position.y + .5f;
        p.lifeSpan = 10;
        p.startScale = 1;
        p.endScale = 0;
        p.damp = .97f;
    }
}
