using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour 
{	
	//0:electron  1:proton  2:neutron (for particle switching)
	public int particleType;

	public GameObject sprite;
	public GameObject avatar;
	public GameObject gameoverScene;
	public GameObject coolDownIndicator;

	private List<GameObject> groundTouched;

	//for level N
	private bool iced = false;

	//for proton
	private bool canMove = true;
	protected bool CanMove
	{
		get
		{
			return canMove;
		}
		set
		{
			canMove = value;
		}
	}

	//attributes of a particle
	public float speed;
	public float jumpSpeed;

	//in order to record the transform of current player
	public GameObject playerTransformObj;  

	//audio
	public GameObject jumpSound;
	public GameObject dieSound;
	public GameObject freezeAudio;

	//for jumping
	private bool inAir;
	private Rigidbody2D rb;

	protected int side = 1;
	protected bool alive = true;

	//for animation
	protected float transformTimer;
	public bool isTransforming;
	protected Animator spriteAnimator;

	private const float SpeedToRotationCoefficient = 0.5f;
	private const float FreezeDuration = 2f;
	private const float FreezeInterval = 2f;

	void FixedUpdate()
	{
		rb.velocity = new Vector2(0f, rb.velocity.y);
	}

	protected virtual void Start()
	{
		groundTouched = new List<GameObject> ();

		spriteAnimator = sprite.GetComponent<Animator>();
		isTransforming = false;

		//for jumping
		rb = GetComponent<Rigidbody2D>();
		inAir = false;

	}

	protected virtual void OnEnable()
	{
		avatar.SetActive(true);
	}

	protected virtual void Update()
	{
		if (iced && GetComponent<Rigidbody2D>().isKinematic) return;

		//get key input
		float horizontal = Input.GetAxis("Horizontal");

		//check if the direction facing is correct
		if (horizontal * side < 0) side *= -1;
		
		//set transform
		if (canMove)
		{
			//transform.Translate(horizontal * Time.deltaTime * speed, 0f, 0f);
			Vector3 newForce = new Vector3(horizontal * speed * Time.deltaTime * 50f, 0f, 0f);
			rb.AddForce(newForce);
			sprite.transform.Rotate(new Vector3(0,0,-1),horizontal * Time.deltaTime * speed * SpeedToRotationCoefficient);
		}

		//check if fire keys are pressed
		if (Input.GetButtonDown("Fire1")) FireBullet1();
		if (Input.GetButtonDown("Fire2")) FireBullet2();

		//check if jumping button pressed
		//print(inAir);
		if (!inAir && canMove && Input.GetButtonDown("Jump"))
		{
			inAir = true;
			rb.AddForce(new Vector2(0f, jumpSpeed));
			Instantiate(jumpSound, transform.position, Quaternion.identity);
		}

		//Check Switch Key
		if (particleType != 2 && canMove && !isTransforming && Input.GetButtonDown("Switch")) Switch();
		
		//update the PlayerTransform object (for the following camera)
		playerTransformObj.transform.position = this.transform.position;
	}

	//reset inAir when the player landed on the ground
	protected virtual void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "Ground")
		{
			groundTouched.Add (col.gameObject);
			inAir = false;
		}
	}

	protected virtual void OnCollisionExit2D(Collision2D col)
	{
		if (col.gameObject.tag == "Ground")
		{
			groundTouched.Remove (col.gameObject);
			if (groundTouched.Count == 0)
				inAir = true;
		}
	}

	protected virtual void Switch()
	{
		isTransforming = true;
	}

	//called everytime when the particle is switched to
	public void Positioning()
	{
		this.transform.position = playerTransformObj.transform.position;
	}

	public bool IsAlive()
	{
		return alive;
	}

	//Freeze() -> ResumeFromFreeze() -> ResetIced()
	public void Freeze()
	{
		if (!iced && !isTransforming) 
		{
			spriteAnimator.SetBool("Iced", true);
			iced = true;
			GetComponent<Rigidbody2D>().isKinematic = true;
			Instantiate(freezeAudio, transform.position, Quaternion.identity);

			Invoke("ResumeFromFreeze", FreezeDuration);
		}
	}

	private void ResumeFromFreeze()
	{
		spriteAnimator.SetBool("Iced", false);
		GetComponent<Rigidbody2D>().isKinematic = false;

		Invoke("ResetIced", FreezeInterval);
	}

	private void ResetIced()
	{
		iced = false;
	}

	protected virtual void FireBullet1(){}

	protected virtual void FireBullet2(){}

	protected virtual void EndSwitch()
	{
		transform.parent = null;
	}

	public virtual void Hurt(float damage){}

	public virtual void HitByLaser(){}

	public virtual void Recover(float amount){}

	/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
	 * I tried to change this script component to attached to a GameObject called "Player" which consists  *
	 * of all electron, proton and neutron.																   *
	 * But failed. (because their's no rigidbody in this component so it is not possible to control the    *
	 * velocity when jumping)																			   *
	 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *

	//attributes of electron,proton,neutron
	public float[] jumpSpeed 	 = new float[3];
	public float[] speed     	 = new float[3];
	public float[] rotationSpeed = new float[3];

	//reference of each paritcle
	public GameObject[] particleRef = new GameObject[3];

	//0:electron  1:proton  2:neutron
	private int state = 0;



	void Update()
	{
		//get key input
		float horizontal = Input.GexAxis("Horizontal");

		//set transform
		transform.Translate(horizontal * Time.deltaTime * speed[state], 0f, 0f);

		//set rotation
		transform.Rotate(0f, 0f, horizontal * Time.deltaTime * rotationSpeed[state]);

		//check if jumping button pressed
		JumpControl();
	}

	void JumpControl()
	{
		if (!inAir && Input.GetButton ("Jump"))
		{
			inAir = true;
			particleRef[state].GetComponent<Rigidbody2D>().velocity = new Vector2(0,*/
}
