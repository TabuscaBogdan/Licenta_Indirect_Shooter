using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile1_behavior : OnHit {


    public float speed = 1.2f;
    // Use this for initialization


    EnemyProjectile1_behavior()
    {
        lifetime = 20;
        this.damage = 10;
    }
    
    void Start () {
        //lifeEnd = Time.time + lifetime;
        BaseStart();
    }
	
	// Update is called once per frame
	void Update () {
        
        Expire();
        transform.position = Vector3.MoveTowards(transform.position, transform.position-transform.forward, maxDistanceDelta: -speed*Time.deltaTime);
        
    }
}
