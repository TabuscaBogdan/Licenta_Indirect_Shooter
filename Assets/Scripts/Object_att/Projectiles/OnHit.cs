using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class OnHit : MonoBehaviour {

    public AudioSource[] sounds;
    public int team;
    public string playerName;

    protected Rigidbody RdBody;
    protected int damage;
    protected Status player_stats;
    // Use this for initialization
    void Start () {
        RdBody = GetComponent<Rigidbody>();
        sounds = gameObject.GetComponents<AudioSource>();
	}
	

    // for closisions
    public void OnCollisionEnter(Collision collision)
    {
        /*problema
         * obiectul dispare si 
         * sunetul nu mai are timp sa ruleze
         * 
         * */
        //sounds[0].transform.parent = null;
        //sounds[0].Play();
        //Destroy(sounds[0].gameObject, sounds[0].clip.length);
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
            Debug.Log("No Problemo");
        }
        finally
        {
            Destroy(gameObject); //remove the projectile
        }

    }
}
