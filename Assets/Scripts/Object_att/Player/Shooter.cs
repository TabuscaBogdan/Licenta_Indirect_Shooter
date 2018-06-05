using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Shooter : NetworkBehaviour {

    // Use this for initialization
    public GameObject projectile;
    public float x, y=1.0f, z;
    public float projSpeed;
    public float cooldown;
    public float cooldown_timer=1.5f;
    private bool available=true;

    private Vector3 offset;
    private Vector3 movement;

    private Status player_stats;
    private OnHit onHit;
    public GameObject player;

	void Start () {
        available = true;
        movement = new Vector3(0.0f, 0.0f, 1.0f);//neds changed
        //-----------------------------------------------------
        //accesul catre altre scripturi ale obiectului
        player_stats = gameObject.GetComponent<Status>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!isLocalPlayer)
        {
            return;
        }
        if (Input.GetMouseButtonDown(0) == true)

        { CmdFire(); }
        
	}
    [Command]
    void CmdFire()
    {
        if (available == true)
        {

            GameObject actuallProjectile = Instantiate(projectile) as GameObject;

            //give ownership
            onHit = projectile.GetComponent<OnHit>();
            onHit.team = player_stats.team;
            onHit.playerName = player_stats.name;
            onHit.SetGoal(player.transform.forward);

            Debug.Log(onHit.transform.position);
            //-----
            //move projectile in front of the player
            offset = player.transform.position;
            offset.y += 1;
            offset += player.transform.forward;
            actuallProjectile.transform.position = offset;

            actuallProjectile.transform.rotation = player.transform.rotation;

            NetworkServer.Spawn(actuallProjectile);

            //-----


            //Rigidbody rb = actuallProjectile.GetComponent<Rigidbody>();
            //rb.AddForce(movement*projSpeed);
            cooldown = Time.time + cooldown_timer;
            available = false;
        }
        if (Time.time > cooldown)
        {
            available = true;
        }
    }
}
