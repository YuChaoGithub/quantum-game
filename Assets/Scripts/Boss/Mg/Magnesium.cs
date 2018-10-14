using UnityEngine;
using System.Collections;

public class Magnesium : Boss
{
	//for shooting
	public GameObject playerPosObj;
	public GameObject bulletPrefab;
	public GameObject beingHitAudio;

	private const float FireIntervalLowerBound = 2f;
	private const float FireIntervalUpperBound = 4f;
	private const float StopTimeBeforeFire = 1f;

	protected override void Start ()
	{
		base.Start ();

		StartCoroutine(fireCycle());
	}

	protected override void Die ()
	{
		base.Die ();

		Destroy(gameObject);
	}

	public override void TakeDamage (int damage)
	{
		base.TakeDamage (damage);

		Instantiate(beingHitAudio, transform.position, Quaternion.identity);
	}

	IEnumerator fireCycle()
	{
		while (base.health > 0f)
		{
			yield return new WaitForSeconds(Random.Range(FireIntervalLowerBound, FireIntervalUpperBound));

			GameObject bullet = (Instantiate(bulletPrefab, transform.position, Quaternion.identity) as GameObject);
			yield return new WaitForSeconds(StopTimeBeforeFire);
			if (bullet == null) continue;
			bullet.GetComponent<MgBullet>().SetObjective(transform.position, playerPosObj.transform.position);
		}
	}
}
