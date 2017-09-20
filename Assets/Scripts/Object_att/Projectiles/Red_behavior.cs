using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Red_behavior : OnHit {

    // Update is called once per frame
    private Vector3 curbe;
    private int lifetime;
    private float rads;
    void spiral(ref Vector3 curb,ref float rads)
    {
        if (lifetime % 2 == 0)
        {
            if (rads >= 6.2f)
            {
                rads = 0.0f;
            }
            rads = rads + 0.1f;
            curb.Set(-Mathf.Cos(rads), 0.0f, -Mathf.Sin(rads));
            curb = curb * 5;
        }
    }
    Red_behavior()
    {
        rads = 0.0f;
        lifetime = 300;
        this.damage = 10;
        curbe = new Vector3(0.0f, 0.0f, 0.0f);
    }
	void Update () {
        spiral(ref curbe,ref rads);
        RdBody.AddForce(curbe);
        lifetime--;
        if(lifetime<0)
        {
            Destroy(gameObject);
        }
	}
}
