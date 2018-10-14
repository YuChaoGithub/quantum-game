using UnityEngine;
using System.Collections;

public class Silicon : Boss
{
	public Sprite[] sprites;
	private Rigidbody2D rb;

	public GameObject beingHitAudio;

	//for movement
	private const float MovementSpeed = 7f;
	private const float JumpSpeed = 6f;
	private const float MovementLowerBound = 0.5f;
	private const float MovementUpperBound = 3f;
	private const float SpeedToRotationCoefficient = 20f;
	private float horizontalMovement = 0f;

	//for tinting
	private const float TintDuration = 0.1f;
	private bool tinting = false;

	protected override void Start ()
	{
		base.Start ();

		rb = GetComponent<Rigidbody2D>();
		StartCoroutine(Cycle());
	}

	protected override void Die ()
	{
		base.Die ();

		base.bottle.transform.position = transform.position;
		Destroy(gameObject);
	}

	void Update()
	{
		transform.Translate(horizontalMovement * Time.deltaTime * MovementSpeed, 0f, 0f);
		base.sprite.transform.Rotate(new Vector3(0,0,-1),horizontalMovement * Time.deltaTime * MovementSpeed * SpeedToRotationCoefficient);
	}

	public override void TakeDamage(int damage)
	{
		if (health == 0) return;

		base.TakeDamage(damage);

		Instantiate(beingHitAudio, transform.position, Quaternion.identity);
		base.sprite.GetComponent<SpriteRenderer>().sprite = sprites[Mathf.RoundToInt(Mathf.Lerp(0f, (float)(sprites.Length - 1), 1 - ((health > 0 ? health : 0) / FullHealth)))];
		if (!tinting) Tint();
	}

	void Tint()
	{
		tinting = true;
		base.sprite.GetComponent<SpriteRenderer>().color = Color.black;

		Invoke("RecoverTint", TintDuration);
	}

	void RecoverTint()
	{
		tinting = false;
		base.sprite.GetComponent<SpriteRenderer>().color = Color.white;
	}

	IEnumerator Cycle()
	{
		while (base.health > 0f)
		{
			yield return new WaitForSeconds(Random.Range(MovementLowerBound, MovementUpperBound));
			switch (Random.Range(0, 5))
			{
			//go left
			case 1:
				horizontalMovement = -1f;
				break;
			
			//go right
			case 2:
				horizontalMovement = 1f;
				break;
			
			//jump
			case 3:
				rb.velocity = new Vector2(rb.velocity.x, JumpSpeed);
				break;

			//stop
			case 4:
				horizontalMovement = 0f;
				break;
			}
		}
	}
}
