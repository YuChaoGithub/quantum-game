using UnityEngine;
using System.Collections;

public class Iron : Boss
{
	public GameObject audio;
	public Sprite hurtSprite;
	private Sprite originalSprite;
	private const float TopLine = 17.8f;
	private const float LeftBound = 220f;
	private const float RightBound = 244.5f;
	private bool tinting = false;
	private const float TintDuration = 0.3f;
	private const float MovementSpeed = 5f;
	private const float RisingSpeed = 2f;
	private const float FallDelayTime = 0.75f;
	private const float RiseDelayTime = 3f;
	private const float RemoveDelay = 8f;

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "Ground")
			Instantiate(audio, transform.position, Quaternion.identity);
	}

	protected override void Start ()
	{
		base.Start ();

		originalSprite = base.sprite.GetComponent<SpriteRenderer>().sprite;
		StartCoroutine(Movement());
	}

	protected override void Die ()
	{
		base.Die ();

		base.bottle.transform.position = transform.position;
		StopAllCoroutines();
		GetComponent<Rigidbody2D>().isKinematic = false;
		GetComponent<BoxCollider2D>().enabled = false;
		Invoke("Remove", RemoveDelay);
	}

	void Remove()
	{
		Destroy(gameObject);
	}

	public override void TakeDamage(int damage)
	{
		if (health == 0) return;

		base.TakeDamage(damage);
		Instantiate(audio, transform.position, Quaternion.identity);

		if (!tinting) Tint();
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

	IEnumerator Movement()
	{
		while (base.health > 0f)
		{
			//move to position on top line
			Vector3 newPos = new Vector3(Random.Range(LeftBound, RightBound), TopLine, 0f);
			float time = Vector3.Distance(transform.position, newPos) / MovementSpeed;
			StartCoroutine(MoveToObjective(transform.position, newPos, time));
			yield return new WaitForSeconds(time);
			StopCoroutine("MoveToObjective");

			//fall
			yield return new WaitForSeconds(FallDelayTime);
			GetComponent<Rigidbody2D>().isKinematic = false;

			//rise
			yield return new WaitForSeconds(RiseDelayTime);
			GetComponent<Rigidbody2D>().isKinematic = true;
			newPos = new Vector3(transform.position.x, TopLine, 0f);
			time = Vector3.Distance(transform.position, newPos) / RisingSpeed;
			StartCoroutine(MoveToObjective(transform.position, newPos, time));
			yield return new WaitForSeconds(time);
			StopCoroutine("MoveToObjective");
		}
	}

	IEnumerator MoveToObjective(Vector3 init, Vector3 final, float time)
	{
		float i = 0f;
		while (i <= 1)
		{
			i += Time.deltaTime/time;
			Vector3 newPos = Vector3.Lerp(init, final, i);
			transform.position = newPos;
			yield return null;
		}
	}
}
