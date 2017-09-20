using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{

    // Use this for initialization
    public int health;
    public string name;
    public int team;

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
        if(health<=0)
        {
            Destroy(gameObject);
        }
    }
}
