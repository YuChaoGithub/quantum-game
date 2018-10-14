using UnityEngine;
using System.Collections;

public class Chromium : Boss
{
	public GameObject beingHitAudio;
	public GameObject throwKnifeAudio;

	public GameObject playerPos;
	public GameObject knifePrefab;
	public GameObject shootPos;
	private const float LowerBound = 11.5f;
	private const float UpperBound = 23f;
	private const float MovementSpeed = 5f;
	private const float ThrowKnifeLowerInterval = 1f;
	private const float ThrowKnifeUpperInterval = 3f;
	private bool tinting = false;
	private const float TintDuration = 0.1f;

	protected override void Start ()
	{
		base.Start ();

		StartCoroutine(ThrowKnife());
		StartCoroutine(Movement());
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

	IEnumerator ThrowKnife()
	{
		while (base.health > 0f)
		{
			yield return new WaitForSeconds(Random.Range(ThrowKnifeLowerInterval, ThrowKnifeUpperInterval));
			Instantiate(throwKnifeAudio, transform.position, Quaternion.identity);
			GameObject knife = (Instantiate(knifePrefab, shootPos.transform.position, Quaternion.identity) as GameObject);
			knife.GetComponent<MgBullet>().SetObjective(shootPos.transform.position, playerPos.transform.position);
		}
	}

	IEnumerator Movement()
	{
		while (base.health > 0f)
		{
			Vector3 newPos = new Vector3(transform.position.x, Random.Range(LowerBound, UpperBound), 0f);
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
}
