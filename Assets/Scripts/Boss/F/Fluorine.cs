using UnityEngine;
using System.Collections;

public class Fluorine : Boss 
{
	//for movement
	private const float LeftBound = 230f;
	private const float RightBound = 244f;
	private const float TopBound = 25f;
	private const float BottomBound = 8.5f;
	private const float MovementSpeed = 3f;
	private const float StopTimeLowerBound = 1f;
	private const float StopTimeUpperBound = 3f;

	//for tinting
	private bool tinting = false;
	private const float TintDuration = 0.1f;

	//audio
	public GameObject beingHitAudio;
	public GameObject appearAudio;
	public GameObject disappearAudio;

	//for animation
	private const float DieTime = 1f;
	private const float BlinkTime = 0.7f;
	private const float StayDuration = 3f;
	private const float DisappearDuration = 2f;

	protected override void Start ()
	{
		base.Start();

		StartCoroutine(Movement());
		StartCoroutine(Blinking());
	}

	protected override void Die ()
	{
		base.Die();

		StartCoroutine(Fade(DieTime));
		Invoke("Remove", DieTime);
	}

	void Remove()
	{
		Destroy(gameObject);
	}

	public override void TakeDamage(int damage)
	{
		if (health == 0) return;

		base.TakeDamage(damage);

		if (!tinting) Tint();
		Instantiate(beingHitAudio, transform.position, Quaternion.identity);
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
			//stop
			yield return new WaitForSeconds(Random.Range(StopTimeLowerBound, StopTimeUpperBound));

			//move
			Vector3 newPos = new Vector3(Random.Range(LeftBound, RightBound), Random.Range(BottomBound, TopBound), -0.1f);
			float time = Vector3.Distance(transform.position, newPos) / MovementSpeed;
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

	IEnumerator Fade(float time, bool back = false)
	{
		float i = 0f;
		while (i <= 1)
		{
			i += Time.deltaTime / time;
			base.sprite.GetComponent<SpriteRenderer>().color = new Color(255f, 255f, 255f, back ? i : (1 - i));
			yield return null;
		}
	}

	IEnumerator Blinking()
	{
		while (health > 0f)
		{
			StartCoroutine(Fade(BlinkTime, false));
			Instantiate(disappearAudio, transform.position, Quaternion.identity);
			yield return new WaitForSeconds(BlinkTime);
			StopCoroutine("Fade");
			GetComponent<PolygonCollider2D>().enabled = false;
			yield return new WaitForSeconds(DisappearDuration);

			StartCoroutine(Fade(BlinkTime, true));
			Instantiate(appearAudio, transform.position, Quaternion.identity);
			yield return new WaitForSeconds(BlinkTime);
			StopCoroutine("Fade");
			GetComponent<PolygonCollider2D>().enabled = true;
			yield return new WaitForSeconds(StayDuration);
		}
	}
}
