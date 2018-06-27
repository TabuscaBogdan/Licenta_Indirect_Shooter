using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EnemyStatus : NetworkBehaviour
{

    public const int max_health = 100;
    public int team;
    public const int value=1;
    public RectTransform healthbar_len;

    public GameObject queryChan;

    [SyncVar(hook = "HealthChange")]
    public int health = max_health;
    public void HealthChange(int health)
    {
        healthbar_len.sizeDelta = new Vector2(health, healthbar_len.sizeDelta.y);
    }

    EnemyStatus()
    {
        team = 0;
        health = 100;
    }
    public void TakeDamage(int amount)
    {
        //this makes it run only on the server
        if (!isServer)
        {
            return;
        }

        health -= amount;
        if (health <= 0)
        {
            FallDown();
        }
    }

    public void FallDown()
    {
        gameObject.GetComponent<EnemyShooter>().enabled = false;
        gameObject.GetComponent<EnemyMovement>().Dissapear();
        gameObject.GetComponent<EnemyMovement>().enabled = false;
        queryChan.GetComponent<QuerySDMecanimController>().ChangeAnimation((QuerySDMecanimController.QueryChanSDAnimationType)58);
        queryChan.GetComponent<QuerySDEmotionalController>().ChangeEmotion((QuerySDEmotionalController.QueryChanSDEmotionalType)4);

        Invoke("GetDestroyed", 3);
    }
    public void GetDestroyed()
    {
        Destroy(gameObject);
    }




}
