using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Networking;

public class OnHit : NetworkBehaviour
{

    public AudioSource[] sounds;
    [SyncVar]
    public int team;
    [SyncVar]
    public string playerName;

    public GameObject afterSound;

    protected Rigidbody RdBody;
    protected int damage;
    protected Status player_stats;
    public Vector3 target_position;
    protected EnemyStatus foe_stats;

    public float lifetime;
    protected float lifeEnd;
    public ScoreMaster scm;

    // Use this for initialization

    public void SetGoal(Vector3 target)//for specific targets
    {
        target_position = target;// - transform.position;
    }

    protected void BaseStart () {
        RdBody = GetComponent<Rigidbody>();
        sounds = gameObject.GetComponents<AudioSource>();
        lifeEnd = Time.time + lifetime;
        scm = GameObject.Find("GameArbiter").GetComponent<ScoreMaster>();
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
                if (player_stats.health > 0 && player_stats.health - damage <= 0)
                {
                    Debug.Log("Player Kill" + playerName + ">" + player_stats.name);
                    Debug.Log(scm);
                }
                player_stats.TakeDamage(damage,playerName);
                
            }
            
        }
        catch (NullReferenceException ex)
        {
            //Debug.Log("Non player collision");
            try
            {
                foe_stats = collision.gameObject.GetComponent<EnemyStatus>();
                //Debug.Log("enters");
                if(team!=foe_stats.team)
                {
                    if (foe_stats.health > 0 && foe_stats.health - damage <= 0)
                    {
                        scm.PlayerTakedown(playerName, "AI");
                    }
                   
                    foe_stats.TakeDamage(damage);
                    
                }
            }
            catch(NullReferenceException except)
            {
                //Debug.Log("World collision");
            }
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
