using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Status : NetworkBehaviour
{

    // Use this for initialization
    public const int max_health=100;
    //Vip to sync here
    //hookul apeleaza o functie atunci cand valoarea se schimba pe client
    [SyncVar(hook = "HealthChange")]
    public int health=max_health;


    public string name;
    public int team;
    public RectTransform healthbar_len;
    Vector2 initial_pozition;

    public void TakeDamage(int amount)
    {
        //this makes it run only on the server
        if(!isServer)
        {
            return;
        }

        health -= amount;
        if (health <= 0)
        {
            Destroy(gameObject);
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
        
    }
    void Update()
    {
        //healthbar_len.sizeDelta = new Vector2(health, healthbar_len.sizeDelta.y);
    }
}
