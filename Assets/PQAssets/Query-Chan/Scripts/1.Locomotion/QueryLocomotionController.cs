using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class QueryLocomotionController : NetworkBehaviour {

	public float moveSpeed			= 1.3f;
	public float dashSpeed			= 4f;
	public float rotationSpeed		= 400f;

	public float jumpPower			= 5f;
	public float jumpInterval		= 1.3f;
	public float gravity			= 0.4f;

    public bool IsFalling;
    public float standardY;
    public int time_falling=0;


    QuerySoundController.QueryChanSoundType[] jumpSounds = {
		QuerySoundController.QueryChanSoundType.ONE_TWO,
		QuerySoundController.QueryChanSoundType.GO_AHEAD,
        QuerySoundController.QueryChanSoundType.CHEER_UP
	};
    QuerySoundController.QueryChanSoundType[] fallSounds =
    {
        QuerySoundController.QueryChanSoundType.OH_NO
    };

	//--------------------------

	CharacterController controller;
	Animator animator;
	QuerySoundController querySound;
	QueryMechanimController queryMechanim;

	//--------------------------

	Vector3 moveDirection		= Vector3.zero;
	float nextAllowedJumpTime	= 0;
    bool touch_down = true;



    //========================================================

    public override void OnStartLocalPlayer()
    {
        Camera.main.GetComponent<CameraControl>().player = gameObject;
    }

    void Start()
	{
		controller = GetComponent<CharacterController>();
		animator = GetComponent<Animator>();
		querySound = GetComponent<QuerySoundController>();
		queryMechanim = GetComponent<QueryMechanimController>();

		queryMechanim.ChangeAnimation(QueryMechanimController.QueryChanAnimationType.IDLE, false);

        IsFalling = false;
        standardY = gameObject.transform.position.y;
        animator.SetBool("Landed", false);

        //set camera to player

    }


	//=========================================================

	void Update()
	{
        if (!isLocalPlayer)
        {
            return;
        }
        updateMove();
	}


	void updateMove()
	{
		float inputX = Input.GetAxis("Horizontal");
		float inputY = Input.GetAxis("Vertical");
		bool dash = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
		bool inputJump = Input.GetButton ("Jump");
		bool jumped = false;

		// movement -----------------------------------
		if (controller.isGrounded) {
			
			moveDirection = new Vector3(0, 0, inputY);
			moveDirection = transform.TransformDirection(moveDirection);

			if (inputY > 0 && dash) {
				moveDirection *= dashSpeed;
			}
			else {
				moveDirection *= moveSpeed;
			}


			if (inputJump && animator.IsInTransition(0) == false && nextAllowedJumpTime <= Time.time) {
				moveDirection.y = jumpPower;
				jumped = true;
				nextAllowedJumpTime = Time.time + jumpInterval;
                touch_down = false;

				PlayJumpSound();
			}


			transform.Rotate(new Vector3(0, inputX * rotationSpeed * Time.deltaTime, 0));
		}
		else {
			moveDirection.y -= gravity;
            
		}
		controller.Move(moveDirection * Time.deltaTime);


		//animation ---------------------------------------
		
        //pt cadere
		animator.SetBool("Jump", jumped);
        if(!controller.isGrounded && animator.IsInTransition(0) == false)
        {
            animator.SetBool("Falling", true);
        }

        if (controller.isGrounded && jumped == false && animator.IsInTransition(0) == false) {
			animator.SetFloat("Speed", inputY * (dash ? 2 : 1));
		}
        
        


    }

	//=====================================================================

	void PlayJumpSound()
	{
		PlaySound( jumpSounds[ Random.Range(0, jumpSounds.Length) ] );
	}
    void PlayFallingSound()
    {
        PlaySound(fallSounds[Random.Range(0, jumpSounds.Length)]);
    }

	void PlaySound(QuerySoundController.QueryChanSoundType soundType)
	{
		querySound.PlaySoundByType(soundType);
	}


}

