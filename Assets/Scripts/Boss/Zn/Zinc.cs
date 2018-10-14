using UnityEngine;
using System.Collections;

public class Zinc : Boss
{
	public GameObject blinkAudio;
	public GameObject beingHitAudio;
	private const float TopBound = 20f;
	private const float BottomBound = 15f;
	private const float LeftBound = 215f;
	private const float RightBound = 245f;
	private const float BlinkTime = 0.5f;
	private bool tinting = false;
	private const float TintDuration = 0.1f;

	protected override void Start ()
	{
		base.Start ();

		StartCoroutine(Blink());
	}

	protected override void Die ()
	{
		base.Die ();

		base.bottle.transform.position = transform.position;
		Destroy(gameObject);
	}

	public override void TakeDamage(int damage)
	{
		if (health == 0) return;
		Instantiate(beingHitAudio, transform.position, Quaternion.identity);
		base.TakeDamage(damage);

		transform.position = new Vector3(Random.Range(LeftBound, RightBound), Random.Range(TopBound, BottomBound), 0f);

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

	IEnumerator Blink()
	{
		while (base.health > 0f)
		{
			SpriteRenderer sr = base.sprite.GetComponent<SpriteRenderer>();
			StartCoroutine(ColorLerp(sr.color, new Color(0f, 0f, 0f, 0f), BlinkTime));
			yield return new WaitForSeconds(BlinkTime);
			StopCoroutine("ColorLerp");

			StartCoroutine(ColorLerp(sr.color, new Color(255f, 255f, 255f, 1f), BlinkTime));
			Instantiate(blinkAudio, transform.position, Quaternion.identity);
			yield return new WaitForSeconds(BlinkTime);
			StopCoroutine("ColorLerp");
		}
	}

	IEnumerator ColorLerp(Color init, Color final, float time)
	{
		float i = 0f;
		while (i <= 1)
		{
			i += Time.deltaTime/time;
			Color newColor = Color.Lerp(init, final, i);
			base.sprite.GetComponent<SpriteRenderer>().color = newColor;
			yield return null;
		}
	}
}
