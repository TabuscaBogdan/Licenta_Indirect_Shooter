using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EnemyShooter : NetworkBehaviour {

    public GameObject projectile;
    public GameObject queryChan;
    private EnemyStatus enemy_stats;
    private OnHit onHit;
    public Vector3 offset;
    //------
    private EnemyMovement AI;

    public float cooldown=3.0f;
    public float available = 0.0f;

    bool shooted = false;
    // Use this for initialization
    void Start () {
        enemy_stats = gameObject.GetComponent<EnemyStatus>();//aici se refera la inamic
    }
	
	// Update is called once per frame
	void Update () {
        AI = gameObject.GetComponent<EnemyMovement>();
        if(AI.inPursuit==true)
        {
            if(Time.time>available)
            {
                available = Time.time + cooldown;

                //anim
                queryChan.GetComponent<QuerySDMecanimController>().ChangeAnimation((QuerySDMecanimController.QueryChanSDAnimationType)106);
                shooted = false;

            }
            if (Time.time > (available - (cooldown / 3)*2))
            {
                if (shooted == false)
                {
                    if (isServer)
                    {
                    Debug.Log("Intra" + Time.time + "---" + (available - 2 * cooldown / 3));
                    
                    offset = transform.position;
                    offset.y = transform.position.y+1;
                    offset += transform.forward;
                    
                    //--
                    //creaza proiectil
                    GameObject actuallProjectile = Instantiate(projectile) as GameObject;

                    //give ownership
                    onHit = projectile.GetComponent<OnHit>();
                    //onHit.SetGoal(AI.player.position);
                    onHit.team = enemy_stats.team;
                    onHit.playerName = "AI";

                    //-----
                    actuallProjectile.transform.position = offset;

                    
                        actuallProjectile.transform.rotation = transform.rotation;
                        NetworkServer.Spawn(actuallProjectile);
                    }


                    shooted = true;
                }
            }
            //for animation to resume
            if (Time.time>(available-cooldown/2))
            {
                queryChan.GetComponent<QuerySDMecanimController>().ChangeAnimation((QuerySDMecanimController.QueryChanSDAnimationType)3);
                queryChan.GetComponent<QuerySDEmotionalController>().ChangeEmotion((QuerySDEmotionalController.QueryChanSDEmotionalType)1);
            }
        }
	}
}
