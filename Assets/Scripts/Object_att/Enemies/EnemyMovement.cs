using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour {

    public GameObject queryChan;

    public Transform player;               // Reference to the player's position.
    int playerHealth;      // Reference to the player's health.
    int enemyHealth;        // Reference to this enemy's health.
    NavMeshAgent nav;               // Reference to the nav mesh agent.
    public float onMeshThreshold = 1;
    public string hunted;    // pe cine vaneaza botul
    public int team;        //echipa in care se afla botul
    private int playerTeam; //referinta la echipa celui vanat

    float normal_speed;
    float normal_acc;

    public bool inPursuit = false;
    public bool dodging = false;



    public float DodgeRate = 0.3f;
    public float NextDodge = 1.0f;

    
    public float wanderRadius=8.0f;//lungimea raze in care ia random o destinatie
    public float wanderTimer;//timpul de plimbare (not in presuit)
    private float wanderStop=0.0f;

    Vector3 newDestination;//ultima pozitie in care caracterul era

    public bool IsAgentOnNavMesh(GameObject agentObject)
    {
        Vector3 agentPosition = agentObject.transform.position;
        NavMeshHit hit;

        // cel mai apropiat punct de agent pe onMeshThreshold
        if (NavMesh.SamplePosition(agentPosition, out hit, onMeshThreshold, NavMesh.AllAreas))
        {
            // verifica daca pozitiile sunt aliniate pe verticala
            if (Mathf.Approximately(agentPosition.x, hit.position.x)
                && Mathf.Approximately(agentPosition.z, hit.position.z))
            {
                // verifica daca obiectul e sub navmesh
                return agentPosition.y >= hit.position.y;
            }
        }

        return false;
    }
    //random movement
    private void WanderAround()
    {
        if(Time.time>wanderStop)
        {

            wanderTimer = Random.value * 7+1.0f;
            wanderStop = Time.time + wanderTimer;


            newDestination = Random.insideUnitSphere * wanderRadius;
            newDestination.y = 0.0f;
            newDestination += gameObject.transform.position;
            nav.SetDestination(newDestination);
            //animation change
            queryChan.GetComponent<QuerySDMecanimController>().ChangeAnimation((QuerySDMecanimController.QueryChanSDAnimationType)2);
            queryChan.GetComponent<QuerySDEmotionalController>().ChangeEmotion((QuerySDEmotionalController.QueryChanSDEmotionalType)5);
        }
        else
        {
            if(transform.position==newDestination)
            {
                queryChan.GetComponent<QuerySDMecanimController>().ChangeAnimation((QuerySDMecanimController.QueryChanSDAnimationType)4);
            }
        }
    }

    private Vector3 SimpleDoddge(Vector3 bulet,Vector3 you,Vector3 hunted)
    {
        Vector3 dir1 =new Vector3(bulet.x, you.y, you.z);
        Vector3 dir2 = new Vector3(you.x, you.y, bulet.z);

        queryChan.GetComponent<QuerySDMecanimController>().ChangeAnimation((QuerySDMecanimController.QueryChanSDAnimationType)10);
        if (Vector3.Distance(hunted, dir1) > Vector3.Distance(hunted, dir2))
        {
            return dir2;
        }
        else
            return dir1;
    }
    private Vector3 SimpleDoddge(Vector3 bulet, Vector3 you)
    {
        Vector3 dir1 = new Vector3(bulet.x, you.y, you.z);
        Vector3 dir2 = new Vector3(you.x, you.y, bulet.z);

        queryChan.GetComponent<QuerySDMecanimController>().ChangeAnimation((QuerySDMecanimController.QueryChanSDAnimationType)10);
        float rnd = Random.value;
        if (rnd > 0.5f)
        {
            return dir1;
        }
        else
            return dir2;
    }

    protected void OnTriggerEnter(Collider foe)
    {
        if(foe.tag.Contains("bullet"))
        {
            if(foe.gameObject.GetComponent<OnHit>().team!=team)
            {
                if(dodging==false)
                {
                    NextDodge = Time.time + DodgeRate;
                    dodging = true;
                    Vector3 dir;
                    nav.acceleration = nav.acceleration * 5;
                    nav.speed = nav.speed * 2;
                    if (inPursuit)
                    {
                       dir = SimpleDoddge(foe.gameObject.transform.position, gameObject.transform.position, player.position);
                    }
                    else
                    {
                        dir = SimpleDoddge(foe.gameObject.transform.position, gameObject.transform.position);
                    }
                    nav.SetDestination(dir);
                }
            }
        }
        if (inPursuit == false)
        {
            if (foe.tag.Contains("Player"))
            {
                player = foe.gameObject.transform;
                playerHealth = player.GetComponent<Status>().health;
                hunted = player.GetComponent<Status>().name;
                playerTeam = player.GetComponent<Status>().team;
                if (playerTeam != team)
                {

                    inPursuit = true;
                    queryChan.GetComponent<QuerySDSoundController>().PlaySoundByNumber(0);//change this
                    queryChan.GetComponent<QuerySDMecanimController>().ChangeAnimation((QuerySDMecanimController.QueryChanSDAnimationType)3);
                    queryChan.GetComponent<QuerySDEmotionalController>().ChangeEmotion((QuerySDEmotionalController.QueryChanSDEmotionalType)1);
                    nav.SetDestination(player.position);
                }
            }
        }
        
    }
    protected void OnTriggerExit(Collider other)
    {
        if (other.tag.Contains("Player"))
        {
            nav.SetDestination(player.position);
            queryChan.GetComponent<QuerySDMecanimController>().ChangeAnimation((QuerySDMecanimController.QueryChanSDAnimationType)3);
            queryChan.GetComponent<QuerySDEmotionalController>().ChangeEmotion((QuerySDEmotionalController.QueryChanSDEmotionalType)3);
            inPursuit = false;
        }
    }
    void Awake()
    {
        // Set up the references.
        //player = GameObject.FindGameObjectWithTag("Player").transform;
        //playerHealth = player.GetComponent<Status>().health;


        nav = gameObject.GetComponent<NavMeshAgent>();
        nav.enabled = true;
        print(nav);
        print(IsAgentOnNavMesh(gameObject));


        normal_speed = nav.speed;
        normal_acc = nav.acceleration;
        //nav.Warp(gameObject.transform.position);
        //animation change
        queryChan.GetComponent<QuerySDMecanimController>().ChangeAnimation((QuerySDMecanimController.QueryChanSDAnimationType)2);
        queryChan.GetComponent<QuerySDEmotionalController>().ChangeEmotion((QuerySDEmotionalController.QueryChanSDEmotionalType)5);

    }


    void Update()
    {
        if (dodging == true)
        {
            
            if (Time.time > NextDodge)
            {
                NextDodge = Time.time + DodgeRate;
                dodging = false;
                nav.speed = normal_speed;
                nav.acceleration = normal_acc;

                //change anim again to presuit
                queryChan.GetComponent<QuerySDMecanimController>().ChangeAnimation((QuerySDMecanimController.QueryChanSDAnimationType)3);
                queryChan.GetComponent<QuerySDEmotionalController>().ChangeEmotion((QuerySDEmotionalController.QueryChanSDEmotionalType)1);
            }
            else
                queryChan.GetComponent<QuerySDMecanimController>().ChangeAnimation((QuerySDMecanimController.QueryChanSDAnimationType)10);
        }
        else
        {
            if (inPursuit == true)
            {
                nav.SetDestination(player.position);
            }
            else
            {
                WanderAround();
                
            }

        }
    }
}
