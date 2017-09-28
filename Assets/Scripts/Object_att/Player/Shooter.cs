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

    private Status player_stats;
    private OnHit onHit;

	void Start () {
        cooldown = 0;
        offset = new Vector3(x, y, z);
        movement = new Vector3(0.0f, 0.0f, 1.0f);//neds changed
        //-----------------------------------------------------
        //accesul catre altre scripturi ale obiectului
        player_stats = gameObject.GetComponent<Status>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0)==true && cooldown==0)
        {
            GameObject actuallProjectile = Instantiate(projectile) as GameObject;

            //give ownership
            onHit = projectile.GetComponent<OnHit>();
            onHit.team = player_stats.team;
            onHit.playerName = player_stats.name;
            //-----

            actuallProjectile.transform.position = transform.position + offset;
            Rigidbody rb = actuallProjectile.GetComponent<Rigidbody>();
            //rb.AddForce(movement*projSpeed);
            cooldown = 60;
        }
        if(cooldown>0)
        {
            cooldown--;
        }
	}
}
