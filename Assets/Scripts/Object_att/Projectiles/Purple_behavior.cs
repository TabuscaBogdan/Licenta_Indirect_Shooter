using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Purple_behavior : OnHit
{

    // Update is called once per frame
    private Vector3 curbe;
    private int lifetime;
    void arch(ref Vector3 curb)
    {
        if (lifetime > 300)
        {
            curb.Set(Random.Range(-1.0f,+1.0f), 0.5f, Random.Range(-1.0f, +1.0f));
        }
        else
            curb.Set(Random.Range(-1.0f, +1.0f), -1.0f, Random.Range(-1.0f, +1.0f));
    }
    Purple_behavior()
    {
        lifetime = 500;
        this.damage = 20;
        curbe = new Vector3(0.0f, 0.0f, 0.0f);
    }
    void Update()
    {
        arch(ref curbe);
        RdBody.AddForce(curbe);
        lifetime--;
        if (lifetime < 0)
        {
            Destroy(gameObject);
        }
    }
}
