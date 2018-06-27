using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Status : NetworkBehaviour
{

    // Use this for initialization
    public const int max_health=100;
    int set = 0;
    bool settled = false;

    public int lives = 5;
    public int takedowns = 0;

    public ScoreMaster scm;
    public string killer;

    //Vip to sync here
    //hookul apeleaza o functie atunci cand valoarea se schimba pe client
    [SyncVar(hook = "HealthChange")]
    public int health=max_health;

    [SyncVar(hook ="OnNameChange")]
    public string name;

    [SyncVar(hook = "OnTeamChange")]
    public int team;

    [SyncVar(hook = "OnColorChange")]
    public Color teamColor;

    public RectTransform healthbar_len;
    Vector2 initial_pozition;

    private NetworkStartPosition[] respawnLocations;

    Animator animator;

    private void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
        
    }

    public void OnColorChange(Color culoare)
    {
        teamColor = culoare;
    }

    public void OnTeamChange(int newTeam)
    {
        team = newTeam;
    }

    public void OnNameChange(string newName)
    {
        name = newName;
        gameObject.transform.GetChild(1).transform.GetChild(0).transform.GetChild(1).GetComponent<Text>().text = newName;
        Debug.Log("NewName" +newName);
    }
    public void SetKiller(string kil)
    {
        Debug.Log("In set Killer" + kil);
        killer = kil;
    }

    public void TakeDamage(int amount,string source)
    {
        //this makes it run only on the server
        if(!isServer)
        {
            return;
        }
        if(health<=0)
        { return; }

        health -= amount;
        if (health <= 0)
        {
            RpcKo(source);
            /*
            if (lives > 0)
            {
                Debug.Log("Reviving is about to begin");
                Invoke("RpcRevive", 5);
            }*/
        }
    }
    public void HealthChange(int health)
    {
        healthbar_len.sizeDelta = new Vector2(health, healthbar_len.sizeDelta.y);
    }


    Status()
    {
        health = 100;
        name = "Unknown";
        team = 0;
    }
    Status(string name,int team)
    {
        this.name = name;
        this.team = team;
        health = 100;
    }
    void Start()
    {
        while (scm == null)
        {
            scm = GameObject.Find("GameArbiter").GetComponent<ScoreMaster>();
        }
        scm.AddPlayer(this);
    }
    [Command]
    public void CmdSetName(string Name)
    {
        name = Name;
    }
    [Command]
    public void CmdGiveMeName()
    {
        RpcGetYourName(name,team,teamColor);
    }
    [Command]
    void CmdReviveMe()
    {
        if (isServer)
        { health = max_health; RpcRevive(); }
    }

    void RefreshInfoGUI()
    {
        gameObject.transform.GetChild(1).transform.GetChild(0).transform.GetChild(1).GetComponent<Text>().text = name;
        gameObject.transform.GetChild(1).transform.GetChild(0).transform.GetChild(1).GetComponent<Text>().color = teamColor;
    }
    void Update()
    {

        if (settled == false)
        { if (name == "Player")
            {
                CmdGiveMeName();
            }
            else
            {
                RefreshInfoGUI();
                settled = true;
            }
        }
        if(scm==null)
        {
            scm = GameObject.Find("GameArbiter").GetComponent<ScoreMaster>();
            Debug.Log("SCM Refresh " + scm);
            scm.AddPlayer(this);
        }
    }
    
    [ClientRpc]
    void RpcGetYourName(string Name,int Team,Color culoare)
    {
        Debug.Log("My new name is " + Name);
        Debug.Log("My new Team is " + Team);

        name = Name;
        team = Team;
        teamColor = culoare;

    }
    [ClientRpc]
    void RpcKo(string source)
    {
        animator.SetBool("KO", true);
        gameObject.GetComponent<Shooter>().enabled = false;
        gameObject.GetComponent<QueryLocomotionController>().enabled = false;
        
        scm.PlayerKilled(name);
        killer = source;
        Debug.Log(killer + "In rpcKO");

        scm.PlayerTakedown(killer, name);

        if (lives > 0)
        {
            Debug.Log("Still got lives");
            Invoke("CmdReviveMe",5);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    [ClientRpc]
    void RpcRevive()
    {
        if(isLocalPlayer)
        {
            Debug.Log("Revive has started");
            Vector3 chosenLocation = new Vector3();
            respawnLocations = FindObjectsOfType<NetworkStartPosition>();

            try
            {
                int k = Random.Range(0, respawnLocations.Length);
                chosenLocation = respawnLocations[k].transform.position;
            }
            catch (System.Exception e)
            {
                Debug.Log(respawnLocations);
                Debug.Log("Either there are no revive locations or something else has happened");
                Debug.Log(e.ToString());
            }
            //--------------------------------------------------
            transform.position = chosenLocation;
            health = max_health;
            animator.SetBool("KO", false);
            gameObject.GetComponent<Shooter>().enabled = true;
            gameObject.GetComponent<QueryLocomotionController>().enabled = true;
            lives--;

        }
    }
}
