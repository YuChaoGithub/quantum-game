using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Boron : Boss 
{
	public GameObject[] dropStonePrefabs;
	public GameObject[] dropStonePositions;
	public GameObject dieGameObj;

	public GameObject beingHitAudio;
	public GameObject spawnBAudio;
	public GameObject tossBAudio;

	private bool tinting = false;
	private const float TintDuration = 0.1f;
	private const float DieTime = 3f;
	private const float ShootIntervalLowerBound = 2.5f;
	private const float ShootIntervalUpperBound = 5f;
	private const float BulletFormationInterval = 0.1f;

	protected override void Start ()
	{
		base.Start ();

		StartCoroutine(ShootCycle());
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
		base.sprite.GetComponent<SpriteRenderer>().color = Color.black;

		Invoke("RecoverTint", TintDuration);
	}

	void RecoverTint()
	{
		tinting = false;
		base.sprite.GetComponent<SpriteRenderer>().color = Color.white;
	}

	protected override void Die ()
	{
		base.Die();

		sprite.SetActive(false);
		dieGameObj.SetActive(true);

		Invoke("Remove", DieTime);
	}

	void Remove()
	{
		Destroy(gameObject);
	}

	IEnumerator ShootCycle()
	{
		while (health > 0) 
		{
			//interval
			yield return new WaitForSeconds(Random.Range(ShootIntervalLowerBound, ShootIntervalUpperBound));

			//set up bullets
			List<GameObject> bullets = new List<GameObject>();
			for (int i = 0; i < dropStonePositions.Length; i++)
			{
				Instantiate(spawnBAudio, transform.position, Quaternion.identity);
				GameObject bullet = (Instantiate(dropStonePrefabs[i], dropStonePositions[i].transform.position, Quaternion.identity) as GameObject);
				bullets.Add(bullet);
				yield return new WaitForSeconds(BulletFormationInterval);
			}

			//fire
			Instantiate(tossBAudio, transform.position, Quaternion.identity);
			foreach (GameObject bullet in bullets)
			{
				if (bullet == null) continue;
				bullet.GetComponent<Rigidbody2D>().isKinematic = false;
				bullet.GetComponent<BBullet>().Shoot();
			}
		}
	}
}
