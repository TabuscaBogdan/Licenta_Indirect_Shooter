using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Red_behavior : OnHit {

    // Update is called once per frame
    private Vector3 position;
    private Vector3 start_point;
    private int lifetime;
    private float rads;
    public float x,z;
    private float pas=0.00001f;
    private float raza=0.1f;
   
    void spiral(ref float raza, ref float pas,ref Vector3 psition)
    {
        
            
            pas +=1.0f;
            if (pas == 360.0f)
            {
                pas = 0.0f;
            }
            raza += 0.0002f + (0.0002f)*pas;
            x = raza * Mathf.Cos(Mathf.PI/180*pas)+start_point.x;
            z = raza * Mathf.Sin(Mathf.PI / 180 * pas)+start_point.z;
            
            position.Set(x, start_point.y, z);

    }
    private void Start()
    {
        start_point = transform.position;
    }
    Red_behavior()
    {
        rads = 0.0f;
        lifetime = 3000;
        this.damage = 10;
        x = 0.1f;
        z = 0.1f;
    }
	void Update () {
        spiral(ref raza, ref pas, ref position);
        transform.position = position;
        lifetime--;
        if(lifetime<0)
        {
            Destroy(gameObject);
        }
	}
}
