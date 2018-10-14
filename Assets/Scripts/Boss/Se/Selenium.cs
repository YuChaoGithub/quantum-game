using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Selenium : Boss
{
	public GameObject beingHitAudio;
	public GameObject bulletAudio;
	public GameObject throwingKnifeAudio;
	public float lowerBound;
	public float upperBound;
	public float timeUpperBound;
	public float timeLowerBound;
	public float stopTimeUpperBound;
	public float stopTimeLowerBound;
	public GameObject dieAnimation;
	public GameObject playerPos;
	public GameObject redShootPos;
	public GameObject[] blackShootPos;
	public GameObject blackBullet;
	public GameObject redBullet;
	private const float RedBulletChance = 0.4f;
	private const float BulletIntervalLowerBound = 1.2f;
	private const float BulletIntervalUpperBound = 3f;
	private const float BulletAppearDelay = 0.5f;
	private bool tinting = false;
	private const float TintDuration = 0.1f;

	IEnumerator Shoot()
	{
		while (base.health > 0f)
		{
			if (Random.value > RedBulletChance)
			{
				List<GameObject> bullets = new List<GameObject>();
				foreach (GameObject shootPos in blackShootPos)
				{
					GameObject bullet = (Instantiate(blackBullet, shootPos.transform.position, Quaternion.identity) as GameObject);
					bullet.GetComponent<Rigidbody2D>().isKinematic = true;
					bullets.Add(bullet);
				}
				yield return new WaitForSeconds(BulletAppearDelay);
				foreach (GameObject bullet in bullets)
				{
					if (bullet != null) bullet.GetComponent<Rigidbody2D>().isKinematic = false;
				}
				Instantiate(bulletAudio, transform.position, Quaternion.identity);
			}
			else
			{
				GameObject bullet = (Instantiate(redBullet, redShootPos.transform.position, Quaternion.identity) as GameObject);
				Instantiate(throwingKnifeAudio, transform.position, Quaternion.identity);
				yield return new WaitForSeconds(BulletAppearDelay);
				bullet.GetComponent<MgBullet>().SetObjective(bullet.transform.position, playerPos.transform.position);
			}
			yield return new WaitForSeconds(Random.Range(BulletIntervalLowerBound, BulletIntervalUpperBound));
		}
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
	protected override void Start ()
	{
		base.Start();

		StartCoroutine(Movement());
		StartCoroutine(Shoot());
	}

	protected override void Die ()
	{
		base.Die();

		base.bottle.transform.position = transform.position;
		Instantiate(dieAnimation, transform.position, Quaternion.identity);
		Destroy(gameObject);
	}

	IEnumerator Movement()
	{
		while (base.health > 0f)
		{
			//randomize movement and time
			Vector3 objective = new Vector3(transform.position.x, Random.Range(lowerBound, upperBound), 0f);
			float time = Random.Range(timeLowerBound, timeUpperBound);
			float stopTime = Random.Range(stopTimeLowerBound, stopTimeUpperBound);

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
