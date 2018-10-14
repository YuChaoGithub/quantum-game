using UnityEngine;
using System.Collections;

public class Phosphorous : Boss
{
	//for fire bug spawning
	public GameObject fireBugPrefab;
	public GameObject leftSpawnPos;
	public GameObject rightSpawnPos;

	public GameObject beingHitAudio;
	public GameObject spawnBugAudio;

	private const float SpawnBugLowerInterval = 3f;
	private const float SpawnBugUpperInterval = 5f;
	private const float SpawnAnimationTime = 1f;
	private const int SpawnNum = 3;

	protected override void Die ()
	{
		base.Die ();

		for (int i = 0; i < SpawnNum * 2; i++)
		{
			Vector3 pos = new Vector3(Random.Range(leftSpawnPos.transform.position.x, rightSpawnPos.transform.position.x), leftSpawnPos.transform.position.y, 0f);
			Instantiate(fireBugPrefab, pos, Quaternion.identity);
		}

		Destroy(gameObject);
	}

	public override void TakeDamage (int damage)
	{
		base.TakeDamage (damage);

		Instantiate(beingHitAudio, transform.position, Quaternion.identity);
	}

	protected override void Start ()
	{
		base.Start ();

		StartCoroutine(Cycle());
	}

	IEnumerator Cycle()
	{
		while (base.health > 0f)
		{
			yield return new WaitForSeconds(Random.Range(SpawnBugLowerInterval, SpawnBugUpperInterval));

			base.sprite.GetComponent<Animator>().SetTrigger("Spawn");
			Instantiate(spawnBugAudio, transform.position, Quaternion.identity);
			yield return new WaitForSeconds(SpawnAnimationTime);

			for (int i = 0; i < SpawnNum; i++)
			{
				Vector3 pos = new Vector3(Random.Range(leftSpawnPos.transform.position.x, rightSpawnPos.transform.position.x), leftSpawnPos.transform.position.y, 0f);
				Instantiate(fireBugPrefab, pos, Quaternion.identity);
			}
		}
	}
}
