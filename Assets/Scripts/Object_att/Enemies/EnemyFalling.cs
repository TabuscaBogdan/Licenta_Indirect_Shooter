using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFalling : MonoBehaviour {

    private bool IsFalling;
    private float standardY;
    public GameObject queryChan;

    
    void Start () {
        IsFalling = false;
        standardY = gameObject.transform.position.y;

    }
	
	// detecteaza daca inamicul este in cadere
	void Update () {
        if (IsFalling == false)
        { if (gameObject.transform.position.y < standardY)
            {
                IsFalling = true;
                queryChan.GetComponent<QuerySDMecanimController>().ChangeAnimation((QuerySDMecanimController.QueryChanSDAnimationType)14);
            }
        }
        if(IsFalling==true)
        {
            if (standardY == gameObject.transform.position.y)
            {
                IsFalling = false;
                queryChan.GetComponent<QuerySDMecanimController>().ChangeAnimation((QuerySDMecanimController.QueryChanSDAnimationType)5);
            }
            else
                standardY = gameObject.transform.position.y;
        }

	}
}
