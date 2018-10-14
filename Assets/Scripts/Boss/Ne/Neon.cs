using UnityEngine;
using System.Collections;

public class Neon : Boss
{
	public Sprite[] sprites;

	private Vector3 leftMostPosition = new Vector3(21.5f, 35f, 0f);
	private Vector3 rightMostPosition = new Vector3(46.5f, 35f, 0f);
	private float side = -1f;

	public GameObject beingHitAudio;

	private const float MinSpinningSpeed = 3f;
	private const float MinSpeed = 2f;
	private const float MaxSpinningSpeed = 6f;
	private const float MaxSpeed = 5f;
	private const float DieAnimationTimer = 1f;

	private Vector3 dieDestination = new Vector3(68f, 35f, 0f);
	private float DieMovingTime = 5f;
	private const float BlinkTime = 0.5f;

	protected override void Start ()
	{
		base.Start ();

		StartCoroutine(Movement());
	}

	protected override void Die ()
	{
		base.Die ();

		base.bottle.transform.position = transform.position;
		GetComponent<CircleCollider2D>().enabled = false;
		base.sprite.GetComponent<SpriteRenderer>().sprite = sprites[0];
		StopAllCoroutines();
		StartCoroutine(Blinking());
		StartCoroutine(MoveToObjective(transform.position, dieDestination, DieMovingTime));
		Invoke("Remove", DieMovingTime);
	}

	void Remove()
	{
		Destroy(gameObject);
	}

	void Update()
	{
		base.sprite.transform.Rotate(0f, 0f, (MinSpinningSpeed + (MaxSpinningSpeed - MinSpinningSpeed) * (1f-(base.health / base.FullHealth))) * side);
	}

	public override void TakeDamage (int damage)
	{
		base.TakeDamage (damage);

		if (health <= 0) return;

		Instantiate(beingHitAudio, transform.position, Quaternion.identity);
		base.sprite.GetComponent<SpriteRenderer>().sprite = sprites[Mathf.RoundToInt(Mathf.Lerp(0f, (float)(sprites.Length - 1), 1 - ((health > 0 ? health : 0) / FullHealth)))];
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
		while (true)
		{
			StartCoroutine(Fade(BlinkTime, false));
			yield return new WaitForSeconds(BlinkTime);
			StopCoroutine("Fade");

			StartCoroutine(Fade(BlinkTime, true));
			yield return new WaitForSeconds(BlinkTime);
			StopCoroutine("Fade");
		}
	}

	IEnumerator Movement()
	{
		while (base.health > 0)
		{
			float time;
			side *= -1;
			//move to left
			time = 10f / (MinSpeed + (MaxSpeed - MinSpeed) * (1f-(base.health / base.FullHealth)));
			StartCoroutine(MoveToObjective(transform.position, leftMostPosition, time));
			yield return new WaitForSeconds(time);
			StopCoroutine("MoveToObjective");

			side *= -1;
			//move to right
			time = 10f / (MinSpeed + (MaxSpeed - MinSpeed) * (1f-(base.health / base.FullHealth)));
			StartCoroutine(MoveToObjective(transform.position, rightMostPosition, time));
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
