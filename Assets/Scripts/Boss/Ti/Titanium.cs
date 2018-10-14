using UnityEngine;
using System.Collections;

public class Titanium : Boss
{
	public GameObject shootPos;
	public GameObject bulPrefabs;

	public GameObject shootAudio;
	public GameObject beingHitAudio;

	private const float ShootLowerInterval = 1f;
	private const float ShootUpperInterval = 3f;
	private const float UpperBound = 76f;
	private const float LowerBound = 39f;
	private bool tinting = false;
	private const float TintDuration = 0.1f;
	private const float MovementSpeed = 12f;

	protected override void Start ()
	{
		base.Start ();

		StartCoroutine(Movement());
		StartCoroutine(Shoot());
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
		base.sprite.GetComponent<SpriteRenderer>().color = Color.red;

		Invoke("RecoverTint", TintDuration);
	}

	void RecoverTint()
	{
		tinting = false;
		base.sprite.GetComponent<SpriteRenderer>().color = Color.white;
	}

	IEnumerator Shoot()
	{
		while (base.health > 0f)
		{
			yield return new WaitForSeconds(Random.Range(ShootLowerInterval, ShootUpperInterval));
			Instantiate(bulPrefabs, shootPos.transform.position, Quaternion.identity);
			Instantiate(shootAudio, transform.position, Quaternion.identity);
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
