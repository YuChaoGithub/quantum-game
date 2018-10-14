using UnityEngine;
using System.Collections;

public class Sodium : Boss
{
	public GameObject beingHitAudio;

	private const float LeftBound = 232f;
	private const float RightBound = 245f;
	private const float YPos = 12f;
	private const float MovementSpeed = 8f;

	private bool tinting = false;
	private const float TintDuration = 0.1f;
	private const float DieAnimationTime = 2f;
	private const float DiePositionY = 4f;

	protected override void Start ()
	{
		base.Start ();

		StartCoroutine(Movement());
	}

	protected override void Die ()
	{
		base.Die ();

		base.bottle.transform.position = new Vector3(transform.position.x, base.bottle.transform.position.y, 0f);

		//die animation
		StopAllCoroutines();
		StartCoroutine(MoveToObjective(transform.position, new Vector3(transform.position.x, DiePositionY, 0f), DieAnimationTime));
		Invoke("Remove", DieAnimationTime);
	}

	void Remove()
	{
		Destroy(gameObject);
	}

	public override void TakeDamage(int damage)
	{
		if (health == 0) return;

		base.TakeDamage(damage);
		Instantiate(beingHitAudio, transform.position, Quaternion.identity);
		if (!tinting) Tint();
	}

	void Tint()
	{
		tinting = true;
		base.sprite.GetComponent<SpriteRenderer>().color = Color.red;

		Invoke("RecoverTint", TintDuration);
	}

	void RecoverTint()
	{
		tinting = false;
		base.sprite.GetComponent<SpriteRenderer>().color = Color.white;
	}

	IEnumerator Movement()
	{
		while (base.health > 0f)
		{
			Vector3 newPos = new Vector3(Random.Range(LeftBound,RightBound), YPos, 0f);
			float time = Vector3.Distance(transform.position, newPos) / MovementSpeed;
			StartCoroutine(MoveToObjective(transform.position, newPos, time));
			yield return new WaitForSeconds(time);
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
