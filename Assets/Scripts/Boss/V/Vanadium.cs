using UnityEngine;
using System.Collections;

public class Vanadium : Boss
{
	public GameObject beingHitAudio;

	private const float UpperBound = 22.08f;
	private const float LowerBound = 21.3f;
	private bool tinting = false;
	private const float TintDuration = 0.1f;
	private const float MoveTime = 1f;
	private const float DieAnimationTime = 1f;
	private Vector3 shrinkSize = new Vector3(0.1f, 0.1f, 0f);

	protected override void Start ()
	{
		base.Start ();

		StartCoroutine(Movement());
	}

	protected override void Die ()
	{
		base.Die ();

		GetComponent<EdgeCollider2D>().enabled = true;
		GetComponent<Rigidbody2D>().isKinematic = false;
		StartCoroutine(Shrink(transform.localScale, shrinkSize, DieAnimationTime));
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
		base.sprite.GetComponent<SpriteRenderer>().color = Color.black;

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
			StartCoroutine(MoveToObjective(transform.position, new Vector3(transform.position.x, UpperBound, 0f), MoveTime));
			yield return new WaitForSeconds(MoveTime);
			StopCoroutine("MoveToObjective");

			StartCoroutine(MoveToObjective(transform.position, new Vector3(transform.position.x, LowerBound, 0f), MoveTime));
			yield return new WaitForSeconds(MoveTime / 2);
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

	IEnumerator Shrink(Vector3 init, Vector3 final, float time)
	{
		float i = 0f;
		while (i <= 1)
		{
			i += Time.deltaTime/time;
			Vector3 newScale = Vector3.Lerp(init, final, i);
			transform.localScale = newScale;
			yield return null;
		}
	}
		
}
