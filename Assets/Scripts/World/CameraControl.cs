using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {
    private Vector3 offset;
    private Vector3 aux=new Vector3();
    public GameObject player;
    public float camera_rotation_speed=2.0f;
    public bool camera_is_set = false;
    Vector3 new_poz;


    void Start () {
        offset = transform.position - player.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
 
    }
    private void LateUpdate()
    {
        /*
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (transform.position.y > 1.0f)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z + 0.5f); 
            }
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (transform.position.y < 10)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z - 0.5f);

            }
        }*/
        float mouseY = Input.GetAxis("Mouse Y");
        float mouseX = Input.GetAxis("Mouse X");


        Quaternion cameraTurnAngle1 = Quaternion.AngleAxis(mouseY * camera_rotation_speed, Vector3.forward);
        Quaternion cameraTurnAngle2 = Quaternion.AngleAxis(mouseX * camera_rotation_speed, Vector3.up);

        offset =cameraTurnAngle2*offset;
        offset = cameraTurnAngle1 * offset;

        try
        { new_poz = player.transform.position + offset; }
        catch (MissingReferenceException)
        {
            Debug.Log("Player is Dead");
        }

        if (new_poz.y > 1.0f)
            transform.position = new_poz;
        else
        {
            new_poz.y = 1.0f;
            transform.position = new_poz;
            offset.y = 1.0f;
        }

        //transform.position = player.transform.position + offset;
        try
        { transform.LookAt(player.transform); }
        catch (MissingReferenceException)
        {
            Debug.Log("Player is Dead");
        }


    }
}
