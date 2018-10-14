using UnityEngine;
using System.Collections;

public class Germanium : Boss
{
	public GameObject beingHitAudio;
	public GameObject tossAudio;

	public GameObject bullet;
	public Vector3 upperBound;
	public Vector3 lowerBound;
	public Vector3 shootForceLowerBound;
	public Vector3 shootForceUpperBound;
	private const float TimeLowerBound = 1f;
	private const float TimeUpperBound = 3f;
	private const float StopTimeLowerBound = 0.5f;
	private const float StopTimeUpperBound = 1.5f;
	private const float ShootIntervalLowerBound = 2f;
	private const float ShootIntervalUpperBound = 5f;
	private bool tinting = false;
	private const float TintDuration = 0.1f;

	protected override void Start ()
	{
		base.Start ();

		StartCoroutine(Movement());
		StartCoroutine(Shoot());
	}

	IEnumerator Shoot()
	{
		while (base.health > 0f)
		{
			float time = Random.Range(ShootIntervalLowerBound, ShootIntervalUpperBound);
			GameObject bul = (Instantiate(bullet, transform.position, Quaternion.identity) as GameObject);
			bul.GetComponent<Rigidbody2D>().AddForce(new Vector3(Random.Range(shootForceLowerBound.x, shootForceUpperBound.x), Random.Range(shootForceLowerBound.y, shootForceUpperBound.y), 0f));
			Instantiate(tossAudio, transform.position, Quaternion.identity);
			yield return new WaitForSeconds(time);
		}
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
	IEnumerator Movement()
	{
		while (base.health > 0f)
		{
			//randomize movement and time
			Vector3 objective = new Vector3(upperBound.x, Random.Range(lowerBound.y, upperBound.y), 0f);
			float time = Random.Range(TimeLowerBound, TimeUpperBound);
			float stopTime = Random.Range(StopTimeLowerBound, StopTimeUpperBound);

			//start moving
			StartCoroutine(MoveToObjective(transform.position, objective, time));

			yield return new WaitForSeconds(time + stopTime);
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
