using UnityEngine;
using System.Collections;

public class GeBoss : Boss
{
	public GameObject beingHitAudio;
	public GameObject bulletPrefab;
	public GameObject shootPos1;
	public GameObject shootPos2;
	public float upperBound;
	public float lowerBound;
	public float movementSpeed;
	public float shootIntervalLowerBound;
	public float shootIntervalUpperBound;
	private bool tinting = false;
	private const float TintDuration = 0.15f;

	protected override void Start ()
	{
		base.Start ();

		StartCoroutine(Shoot());
		StartCoroutine(Movement());
	}

	protected override void Die ()
	{
		base.Die ();

		bottle.transform.position = transform.position;
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
			yield return new WaitForSeconds(Random.Range(shootIntervalLowerBound, shootIntervalUpperBound));
			Instantiate(bulletPrefab, Random.value > .5f ? shootPos1.transform.position : shootPos2.transform.position, Quaternion.identity);
		}
	}

	IEnumerator Movement()
	{
		while (base.health > 0f)
		{
			Vector3 newPos = new Vector3(transform.position.x, Random.Range(upperBound, lowerBound), 0f);
			float time = Vector3.Distance(transform.position, newPos) / movementSpeed;
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
