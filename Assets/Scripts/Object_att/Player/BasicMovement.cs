using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour
{

    private Rigidbody RdBody;
    public float speedMultiplyer=1.0f;
    public float jump;
    public float jumpDistance = 200.0f;
    private int jumpCooldown=0;
    private Vector3 stop = new Vector3(0.0f, 0.0f, 0.0f);
    // Use this for initialization
    void Start()
    {

        RdBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        float moveVerticalWS;
        float moveHorizontalAD;
        //--------
        moveVerticalWS = Input.GetAxis("Vertical");
        moveHorizontalAD = Input.GetAxis("Horizontal");
        jump = Input.GetAxis("Jump");

        Vector3 movement = new Vector3(moveHorizontalAD , 0.0f, moveVerticalWS);
        Vector3 jumper = new Vector3(0.0f, jumpDistance, 0.0f);


        //---------------------------
        //ca sa nu poti zbura la infinit
        if(jumpCooldown>0)
        jumpCooldown -= 1;
        //---------------------------



        if (jump < 1.0)
            RdBody.AddForce(movement * speedMultiplyer);
        else
        {
            if (jumpCooldown == 0)
            {
                RdBody.AddForce(jumper);
                jumpCooldown = 60;// pt ca jocul merge in 60 de fps
                                  // trebuie ajustat pt frame rate dropuri...
            }
        }
    }
}
