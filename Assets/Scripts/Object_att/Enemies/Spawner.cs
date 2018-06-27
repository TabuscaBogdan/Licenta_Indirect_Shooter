using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Spawner : NetworkBehaviour
{
    public GameObject enemyToSpawn;
    private Vector3 spawnPosition;

    public bool strongerSpawns = false;
    public bool startSpawning = false;

    private int no_spawns = 1;

    //===Timings
    public float spawnCooldown = 30.0f;
    public float nextSpawn;
    public float strongSpawnCooldown = 60.0f;
    public float nextStrongSpawn=0.0f;



    public void SpawnSimpleEnemy()
    {
        Vector3 sPosition;
        Quaternion sRotation;

        sRotation=Quaternion.Euler(0.0f,Random.Range(0, 360),0.0f);
        sPosition= new Vector3(Random.Range(-5.0f+spawnPosition.x, 5.0f+spawnPosition.x),
            spawnPosition.y,
            Random.Range(-5.0f+spawnPosition.z, 5.0f+spawnPosition.z));

        var enemy = Instantiate(enemyToSpawn, sPosition, sRotation);
        NetworkServer.Spawn(enemy);

    }

    public void TimedSpawn()
    {
        if(Time.time<nextSpawn)
        {
            return;
        }
        if(strongerSpawns)
        {
            if(Time.time>(nextStrongSpawn+strongSpawnCooldown))
            {
                nextStrongSpawn += strongSpawnCooldown;
                no_spawns++;
            }
        }
        for(int i=0;i<no_spawns;i++)
        {
            SpawnSimpleEnemy();
        }
        nextSpawn += spawnCooldown;
    }

    public override void OnStartServer()
    {
        spawnPosition = gameObject.transform.position;
        SpawnSimpleEnemy();
        nextSpawn = Time.time+spawnCooldown;
        startSpawning = true;
    }

    private void Update()
    {
        if (startSpawning)
            TimedSpawn();
    }

}
