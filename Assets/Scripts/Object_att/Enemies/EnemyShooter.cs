using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour {

    public GameObject projectile;
    public GameObject queryChan;
    private Status player_stats;
    private OnHit onHit;
    public Vector3 offset;
    //------
    private EnemyMovement AI;

    public float cooldown=3.0f;
    public float available = 0.0f;

    bool shooted = false;
    // Use this for initialization
    void Start () {
        player_stats = gameObject.GetComponent<Status>();//aici se refera la inamic
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
                    Debug.Log("Intra" + Time.time + "---" + (available - 2 * cooldown / 3));
                    var targetDirection = AI.player.position - gameObject.transform.position;
                    offset.x = targetDirection.x % 1;
                    offset.y = targetDirection.y % 1;
                    offset.z = targetDirection.z % 1;
                    //--
                    //creaza proiectil
                    GameObject actuallProjectile = Instantiate(projectile) as GameObject;

                    //give ownership
                    onHit = projectile.GetComponent<OnHit>();
                    onHit.SetGoal(AI.player.position);
                    onHit.team = player_stats.team;
                    onHit.playerName = player_stats.name;

                    //-----
                    actuallProjectile.transform.position = transform.position + offset;

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
