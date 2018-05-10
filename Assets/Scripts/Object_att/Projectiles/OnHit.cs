using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class OnHit : MonoBehaviour {

    public AudioSource[] sounds;
    public int team;
    public string playerName;
    public GameObject afterSound;

    protected Rigidbody RdBody;
    protected int damage;
    protected Status player_stats;
    public Vector3 target_position;
    // Use this for initialization

    public void SetGoal(Vector3 target)//for specific targets
    {
        target_position = target;// - transform.position;
    }

    void Start () {
        RdBody = GetComponent<Rigidbody>();
        sounds = gameObject.GetComponents<AudioSource>();
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
            if (player_stats.team != team)
            {
                player_stats.health -= damage;
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
}
