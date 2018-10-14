using UnityEngine;
using System.Collections;

public class Argon : Boss
{
	public GameObject appearAudio;
	public GameObject disappearAudio;
	public GameObject beingHitAudio;

	private SpriteRenderer renderer;
	private bool tinting = false;
	private const float TintDuration = 0.1f;
	private const float DisappearAnimationTime = 0.5f;
	private const float DisappearTimeLowerBound = 1f;
	private const float DisappearTimeUpperBound = 2f;
	private const float RevealTimeLowerBound = 2f;
	private const float RevealTimeUpperBound = 3f;
	private const float DieAnimationTime = 3f;
	private const float UpperBound = 39f;
	private const float RightBound = 17f;
	private const float LeftBound = 2f;
	private const float LowerBound = 32f;
	private const float MovementSpeed = 3f;
	private Vector3 deathPosition = new Vector3(-12f, 32f, 0f);

	protected override void Start ()
	{
		base.Start ();
		renderer = base.sprite.GetComponent<SpriteRenderer>();

		StartCoroutine(Movement());
		StartCoroutine(Disappear());
	}

	protected override void Die ()
	{
		base.Die ();

		base.bottle.transform.position = transform.position;
		StartCoroutine(MoveToObjective(transform.position, deathPosition, DieAnimationTime));
		StartCoroutine(AdjustAlpha(1f, 0.3f, DieAnimationTime));
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
		renderer.color = Color.red;

		Invoke("RecoverTint", TintDuration);
	}

	void RecoverTint()
	{
		tinting = false;
		renderer.color = Color.white;
	}

	IEnumerator Movement()
	{
		while (base.health > 0f) {
			//move to objective
			Vector3 newPos = new Vector3(Random.Range(LeftBound, RightBound), Random.Range(LowerBound, UpperBound), 0f);
			float time = Vector3.Distance(transform.position, newPos) / MovementSpeed;
			StartCoroutine(MoveToObjective(transform.position, newPos, time));
			yield return new WaitForSeconds(time);
			StopCoroutine("MoveToObjective");
		}
	}

	IEnumerator Disappear()
	{
		while (base.health > 0f) {
			yield return new WaitForSeconds(Random.Range(RevealTimeLowerBound, RevealTimeUpperBound));

			//disappear animation
			StartCoroutine(AdjustAlpha(1f, 0f, DisappearAnimationTime));
			Instantiate(disappearAudio, transform.position, Quaternion.identity);
			yield return new WaitForSeconds(DisappearAnimationTime);
			StopCoroutine("AdjustAlpha");

			//disappear
			GetComponent<PolygonCollider2D>().enabled = false;
			yield return new WaitForSeconds(Random.Range(DisappearTimeLowerBound, DisappearTimeUpperBound));

			//appear animation
			StartCoroutine(AdjustAlpha(0f, 1f, DisappearAnimationTime));
			Instantiate(appearAudio, transform.position, Quaternion.identity);
			yield return new WaitForSeconds(DisappearAnimationTime);
			StopCoroutine("AdjustAlpha");

			//appear
			GetComponent<PolygonCollider2D>().enabled = true;
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

	IEnumerator AdjustAlpha(float init, float final, float time)
	{
		float i = 0f;
		while (i <= 1)
		{
			i += Time.deltaTime/time;
			Color newColor = new Color(renderer.color.r, renderer.color.g, renderer.color.b, Mathf.Lerp(init, final, i));
			renderer.color = newColor;
			yield return null;
		}
	}
}
