using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour {

    // Use this for initialization
    public GameObject projectile;
    public float x, y, z;
    public float projSpeed;
    private int cooldown;
    private Vector3 offset;
    private Vector3 movement;
	void Start () {
        cooldown = 0;
        offset = new Vector3(x, y, z);
        movement = new Vector3(1.0f, 0.0f, 1.0f);//neds changed
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0)==true && cooldown==0)
        {
            GameObject actuallProjectile = Instantiate(projectile) as GameObject;
            actuallProjectile.transform.position = transform.position + offset;
            Rigidbody rb = actuallProjectile.GetComponent<Rigidbody>();
            rb.AddForce(movement*projSpeed);
            cooldown = 60;
        }
        if(cooldown>0)
        {
            cooldown--;
        }
	}
}
