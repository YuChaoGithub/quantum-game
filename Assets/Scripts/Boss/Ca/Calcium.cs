using UnityEngine;
using System.Collections;

public class Calcium : Boss
{
	public GameObject bulletPrefab1;
	public GameObject bulletPrefab2;
	public GameObject shootPos;
	public Sprite hurtSprite;
	public Sprite originalSprite;

	public GameObject tossBoneAudio;
	public GameObject beingHitAudio;

	private bool tinting = false;
	private Rigidbody2D rb;
	private float horizontalMovement = 0f;
	private const float MovementSpeed = 4f;
	private const float JumpSpeedLowerBound = 4f;
	private const float JumpSpeedUpperBound = 8f;
	private const float TintDuration = 0.1f;
	private const float LeftBound = 172.5f;
	private const float RightBound = 189f;
	private const float JumpIntervalLowerBound = 3f;
	private const float JumpIntervalUpperBound = 5f;
	private const float JumpDelayLowerBound = 0.25f;
	private const float JumpDelayUpperBound = 0.75f;

	protected override void Start ()
	{
		base.Start ();

		rb = GetComponent<Rigidbody2D>();

		StartCoroutine(Movement());
		StartCoroutine(Jump());
	}

	protected override void Die ()
	{
		base.Die ();

		base.bottle.transform.position = transform.position;
		ThrowBones();

		Destroy(gameObject);
	}

	public override void TakeDamage(int damage)
	{
		if (health == 0) return;

		base.TakeDamage(damage);

		Instantiate(beingHitAudio, transform.position, Quaternion.identity);
		if (!tinting) Tint();
	}

	void Update()
	{
		transform.Translate(horizontalMovement * Time.deltaTime * MovementSpeed, 0f, 0f);
	}

	void Tint()
	{
		tinting = true;
		base.sprite.GetComponent<SpriteRenderer>().sprite = hurtSprite;

		Invoke("RecoverTint", TintDuration);
	}

	void RecoverTint()
	{
		tinting = false;
		base.sprite.GetComponent<SpriteRenderer>().sprite = originalSprite;
	}

	void ThrowBones()
	{
		Instantiate(tossBoneAudio, transform.position, Quaternion.identity);
		Instantiate(bulletPrefab1, shootPos.transform.position, Quaternion.identity);
		Instantiate(bulletPrefab2, shootPos.transform.position, Quaternion.identity);
	}

	IEnumerator Movement()
	{
		while (base.health > 0f)
		{
			float newX = Random.Range(LeftBound, RightBound);
			float time = Mathf.Abs(newX - transform.position.x) / MovementSpeed;
			StartCoroutine(MoveToObjective(transform.position.x, newX, time));
			yield return new WaitForSeconds(time);
			StopCoroutine("MoveToOjbective");
		}
	}
		
	IEnumerator Jump()
	{
		while (base.health > 0f)
		{
			yield return new WaitForSeconds(Random.Range(JumpIntervalLowerBound, JumpIntervalUpperBound));
			rb.velocity = new Vector2(rb.velocity.x, Random.Range(JumpSpeedLowerBound, JumpSpeedUpperBound));
			yield return new WaitForSeconds(Random.Range(JumpDelayLowerBound, JumpDelayUpperBound));
			ThrowBones();
		}
	}

	IEnumerator MoveToObjective(float initX, float finalX, float time)
	{
		float i = 0f;
		while (i <= 1)
		{
			i += Time.deltaTime/time;
			Vector3 newPos = new Vector3(Mathf.Lerp(initX, finalX, i), transform.position.y, 0f);
			transform.position = newPos;
			yield return null;
		}
	}
}
