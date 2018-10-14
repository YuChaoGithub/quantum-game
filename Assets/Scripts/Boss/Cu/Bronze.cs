using UnityEngine;
using System.Collections;

public class Bronze : Boss
{
	public GameObject beingHitAudio;
	private const float TurnDegree = 10f;
	private const float TurnTime = 0.5f;
	private Vector3 LeftBound = new Vector3(231f, 11.43f, 0f);
	private Vector3 RightBound = new Vector3(244f, 11.43f, 0f);
	private const float MovementTime = 2f;
	private bool tinting = false;
	private const float TintDuration = 0.2f;

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
		base.sprite.GetComponent<SpriteRenderer>().color = new Color(144f, 45f, 45f);
	}

	protected override void Start ()
	{
		base.Start ();

		StartCoroutine(Turning());
		StartCoroutine(Movement());
	}

	protected override void Die ()
	{
		base.Die ();

		base.bottle.transform.position = transform.position;
		Destroy(gameObject);
	}

	IEnumerator Movement()
	{
		while (base.health > 0f)
		{
			StartCoroutine(MoveToObjective(transform.position, RightBound, MovementTime));
			yield return new WaitForSeconds(MovementTime);
			StopCoroutine("MoveToObjective");
			StartCoroutine(MoveToObjective(transform.position, LeftBound, MovementTime));
			yield return new WaitForSeconds(MovementTime);
			StopCoroutine("MoveToObjective");
		}
	}

	IEnumerator Turning()
	{
		while (base.health > 0f)
		{
			StartCoroutine(TurnTo(TurnDegree, -TurnDegree, TurnTime));
			yield return new WaitForSeconds(TurnTime);
			StopCoroutine("TurnTo");
			StartCoroutine(TurnTo(-TurnDegree, TurnDegree, TurnTime));
			yield return new WaitForSeconds(TurnTime);
			StopCoroutine("TurnTo");
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

	IEnumerator TurnTo(float init, float final, float time)
	{
		float i = 0f;
		while (i <= 1)
		{
			i += Time.deltaTime/time;
			Quaternion newQua = Quaternion.Euler(new Vector3(0f, 0f, Mathf.Lerp(init, final, i)));
			transform.rotation = newQua;
			yield return null;
		}
	}
}
