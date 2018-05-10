using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Green_behaivior : OnHit {

    private Vector3 position;
    private Vector3 start_point;
    private int lifetime;
    private Vector3 normalizeDirection;

    public float speed = 5.0f;
    // Use this for initialization


    Green_behaivior()
    {
        lifetime = 3000;
        this.damage = 10;
    }
    
    void Start () {
        normalizeDirection = (target_position - transform.position).normalized;
    }
	
	// Update is called once per frame
	void Update () {
        //Debug.Log(target_position + "Ovah Here");
        //transform.position += normalizeDirection * speed * Time.deltaTime;
        //transform.position = Vector3.MoveTowards(transform.position, target_position, -1.0f);
        transform.position = target_position;// - normalizeDirection;
        lifetime--;
        if (lifetime < 0)
        {
            Destroy(gameObject);
        }
    }
}
