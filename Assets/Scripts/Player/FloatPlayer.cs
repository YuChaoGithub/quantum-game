using UnityEngine;
using System.Collections;

//a big mistake not to plan before programming..... this class is like 90% the same as the Player class
public class FloatPlayer : MonoBehaviour 
{
	public int particleType;

	public GameObject sprite;
	public GameObject avatar;
	public GameObject gameoverScene;
	public GameObject coolDownIndicator;
	public GameObject bubble;
	public GameObject bubblePositionLeft;
	public GameObject bubblePositionRight;
	public GameObject dieAudio;

	public float health;
	public GameObject healthBar;
	public GameObject beingHitAudio;
	
	private bool canBeHurtByLaser = true;
	private float fullHealth;
	private bool hurt = false;
	
	private const float HurtTime = 1f;
	private const float LaserHitInterval = 0.1f;
	private const float TransformTime = 0.5f;
	private const float BubbleInterval = 3f;

	//for proton
	private bool canMove = true;
	public bool CanMove
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
	public float speed;

	private Rigidbody2D rb { get { return GetComponent<Rigidbody2D> (); } }

	//record the transform of player
	public GameObject playerTransformObj;

	protected int side = 1;
	protected bool alive = true;

	//for animation
	protected float transformTimer;
	protected bool isTransforming = false;
	protected Animator spriteAnimator;

	void FixedUpdate()
	{
		rb.velocity = new Vector2(0f, 0f);
	}

	protected virtual void Start()
	{
		spriteAnimator = sprite.GetComponent<Animator>();

		fullHealth = health;

	}

	protected virtual void OnEnable()
	{
		avatar.SetActive(true);

		InvokeRepeating("GenerateBubble", BubbleInterval, BubbleInterval);
	}

	void GenerateBubble()
	{
		if (side == 1)
		{
			Instantiate(bubble, bubblePositionRight.transform.position, Quaternion.identity);
		}
		else
		{
			Instantiate(bubble, bubblePositionLeft.transform.position, Quaternion.identity);
		}
	}

	protected virtual void Update()
	{
		//get key input
		float horizontal = Input.GetAxis("Horizontal");
		float vertical = Input.GetAxis("Vertical");
		
		//check if the direction facing is correct
		if (horizontal * side < 0) 
		{
			side *= -1;
			Vector3 scale = sprite.transform.localScale;
			scale.x *= -1;
			sprite.transform.localScale = scale;
		}
		
		//set transform
		if (canMove)
		{
			Vector3 newForce = new Vector3(horizontal * speed * Time.deltaTime * 50f, vertical * speed, 0f);
			rb.AddForce(newForce);
		}
		
		//check if fire keys are pressed
		if (Input.GetButtonDown("Fire1")) FireBullet1();
		if (Input.GetButtonDown("Fire2")) FireBullet2();

		//Check Switch Key
		if (canMove && !isTransforming && Input.GetButtonDown("Switch")) Switch();
		
		//update the PlayerTransform object (for the following camera)
		playerTransformObj.transform.position = this.transform.position;
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

	void OnCollisionStay2D(Collision2D col)
	{	
		//hurt by monster
		if (!hurt && col.gameObject.tag == "Float Monster")
		{
			float damage = col.gameObject.GetComponent<FloatEnemy>().damage;
			
			hurt = true;
			
			Hurt (damage);
		}
	}

	void ResetHurt()
	{
		if (!IsAlive () || isTransforming)
			return;
		hurt = false;
		spriteAnimator.SetBool("Hurt", false);
	}

	protected virtual void Die()
	{
		Instantiate (dieAudio, transform.position, Quaternion.identity);
		alive = false;
	}
	
	public void HitByLaser()
	{
		if (canBeHurtByLaser)
		{
			canBeHurtByLaser = false;
			Invoke("ResetCanBeHurtByLaser", LaserHitInterval);
			Hurt(5f);
		}
	}
	
	void ResetCanBeHurtByLaser()
	{
		canBeHurtByLaser = true;
	}
	
	public void Recover(float amount)
	{
		if (health + amount <= fullHealth)
		{
			health += amount;
			healthBar.GetComponent<HealthBar>().AddValue(amount);
		}
		else
		{
			health = fullHealth;
			healthBar.GetComponent<HealthBar>().SetValue(fullHealth);
		}
	}
	
	protected virtual void Switch()
	{
		isTransforming = true;
		
		Invoke("ResetIsTransforming", TransformTime);
	}
	
	void ResetIsTransforming()
	{
		isTransforming = false;
		EndSwitch();
	}
	
	protected virtual void FireBullet1(){}
	
	protected virtual void FireBullet2(){}
	
	protected virtual void EndSwitch()
	{
		transform.parent = null;

		Color c = Color.white;
		sprite.GetComponent<SpriteRenderer>().color = c;

		CancelInvoke("GenerateBubble");
	}
	
	public void Hurt(float damage)
	{
		if (!IsAlive ())
			return;

		if (health - damage <= 0) {
			healthBar.GetComponent<HealthBar> ().SetValue (0);
			Die ();
		}
		else 
		{
			Instantiate (beingHitAudio, transform.position, Quaternion.identity);
			health -= damage;
			healthBar.GetComponent<HealthBar> ().MinusValue (damage);
		
			spriteAnimator.SetBool ("Hurt", true);
			Invoke ("ResetHurt", HurtTime);
		}
	}

}
