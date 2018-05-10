using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {
    private Vector3 offset;
    private Vector3 aux=new Vector3();
    public GameObject player;
    public float camera_rotation_speed=4.0f;


    void Start () {
        offset = transform.position - player.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
 
    }
    private void LateUpdate()
    {
        
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
        }
        Quaternion cameraTurnAngle1 = Quaternion.AngleAxis(Input.GetAxis("Mouse Y") * camera_rotation_speed, Vector3.forward);
        Quaternion cameraTurnAngle2 = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * camera_rotation_speed, Vector3.up);

        offset =cameraTurnAngle2*offset;
        offset = cameraTurnAngle1 * offset;
        transform.position = player.transform.position + offset;
        transform.LookAt(player.transform);

        
    }
}
