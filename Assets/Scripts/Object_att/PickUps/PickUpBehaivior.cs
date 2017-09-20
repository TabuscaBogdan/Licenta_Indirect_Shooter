using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class PickUpBehaivior : MonoBehaviour {

    public GameObject proj_type;
    private Rigidbody RdBody;
    private Shooter shooter;
    // Use this for initialization
    void Start () {
        RdBody = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(new Vector3 (15,30,45) * Time.deltaTime);
    }
    private void OnCollisionEnter(Collision collision)
    {
        try
        {
            shooter = collision.gameObject.GetComponent<Shooter>();
            shooter.projectile = proj_type;

        }
        catch (NullReferenceException ex)
        {
            Debug.Log("No Problemo PickUp");
        }
        finally
        {
            Destroy(gameObject);
        }

    }
}
