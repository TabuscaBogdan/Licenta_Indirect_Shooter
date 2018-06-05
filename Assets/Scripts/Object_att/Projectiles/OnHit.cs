using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Networking;

public class OnHit : NetworkBehaviour
{

    public AudioSource[] sounds;
    public int team;
    public string playerName;
    public GameObject afterSound;

    protected Rigidbody RdBody;
    protected int damage;
    protected Status player_stats;
    public Vector3 target_position;

    public float lifetime;
    protected float lifeEnd;

    // Use this for initialization

    public void SetGoal(Vector3 target)//for specific targets
    {
        target_position = target;// - transform.position;
    }

    void Start () {
        RdBody = GetComponent<Rigidbody>();
        sounds = gameObject.GetComponents<AudioSource>();
        lifeEnd = Time.time + lifetime;
	}
	

    // for closisions
    public void OnCollisionEnter(Collision collision)
    {
        try
        {
            GameObject hitSound = Instantiate(afterSound) as GameObject;
            afterSound.transform.parent = null;
        }
        catch(UnassignedReferenceException)
        {
            Debug.Log("Sound Efect");
        }

        //verify if friend or foe
        try
        {
            player_stats = collision.gameObject.GetComponent<Status>();
            if (player_stats.team != team || team==0)
            {
                player_stats.TakeDamage(damage);
                //player_stats.health -= damage; //depricated
            }
            
        }
        catch (NullReferenceException ex)
        {
            Debug.Log("Non player collision");
        }
        finally
        {
            Destroy(gameObject); //remove the projectile
        }

    }
    protected void Expire()
    {
        if(Time.time>lifeEnd)
        {
            Destroy(gameObject);
        }
    }

}
