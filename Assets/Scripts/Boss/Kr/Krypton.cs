using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Krypton : Boss
{
	public GameObject beingHitAudio;
	public GameObject spawnBulletAudio;
	public GameObject dropBulletAudio;
	public GameObject spawnMonsterAudio;
	public float leftBound;
	public float rightBound;
	public float lowerBound;
	public float upperBound;
	public GameObject[] throwingStuffs;
	public GameObject krBombPrefab;
	public GameObject[] krBombLocations;
	public GameObject hitAnimation;
	public GameObject hitAreaEffector;
	public GameObject dieAnimation;
	public float bombForceLowerX;
	public float bombForceUpperX;
	public float bombForceLowerY;
	public float bombForceUpperY;
	private const float krBombDelay = 0.05f;
	private const float RotateBackTime = 0.5f;
	private const float ExpandScale = 1.25f;
	private const float ExpandTime = 0.75f;
	private const float ShrinkTime = 0.25f;
	private const float MovingSpeed = 15f;
	private const float RotatingSpeed = 500f;
	private bool rotating = false;
	private const int ExpandTimes = 5;

	protected override void Start ()
	{
		base.Start ();

		StartCoroutine(Cycle());
	}

	void Update()
	{
		if (rotating) transform.Rotate(0f, 0f, Time.deltaTime * RotatingSpeed);
	}

	protected override void Die ()
	{
		base.Die ();

		base.bottle.transform.position = transform.position;
		Instantiate(dieAnimation, transform.position, Quaternion.identity);
		Instantiate(hitAreaEffector, transform.position, Quaternion.identity);
		Destroy(gameObject);
	}

	public override void TakeDamage (int damage)
	{
		base.TakeDamage (damage);

		Instantiate(beingHitAudio, transform.position, Quaternion.identity);
		Instantiate(hitAnimation, transform.position, transform.rotation);
		Instantiate(hitAreaEffector, transform.position, Quaternion.identity);
	}

	IEnumerator Cycle()
	{
		while (base.health > 0f)
		{
			//move to a location
			Vector3 newPos = new Vector3(Random.Range(leftBound, rightBound), Random.Range(lowerBound, upperBound), 0f);
			float time = Vector3.Distance(transform.position, newPos) / MovingSpeed;
			rotating = true;
			StartCoroutine(MoveToObjective(transform.position, newPos, time));
			yield return new WaitForSeconds(time);
			StopCoroutine("MoveToObjective");
			rotating = false;
			StartCoroutine(RotateBack());
			yield return new WaitForSeconds(RotateBackTime);
			StopCoroutine("RotateBack");

			//stop for a while
			for (int i = 0; i < ExpandTimes; i++)
			{
				//expand
				StartCoroutine(ScaleLerp(1f, ExpandScale, ExpandTime));
				yield return new WaitForSeconds(ExpandTime);
				StopCoroutine("ScaleLerp");

				//Throw a thing or bomb
				if (Random.value > 0.5f)
				{
					Instantiate(throwingStuffs[Random.Range(0, throwingStuffs.Length)], transform.position, Quaternion.identity);
					Instantiate(spawnMonsterAudio, transform.position, Quaternion.identity);
				}
				else
				{
					List<GameObject> bombs = new List<GameObject>();
					foreach (GameObject pos in krBombLocations)
					{
						bombs.Add((Instantiate(krBombPrefab, pos.transform.position, Quaternion.identity) as GameObject));
						Instantiate(spawnBulletAudio, transform.position, Quaternion.identity);
						yield return new WaitForSeconds(krBombDelay);
					}
					foreach (GameObject bomb in bombs)
					{
						if (bomb == null)
							continue;
						bomb.GetComponent<Rigidbody2D>().isKinematic = false;
						bomb.GetComponent<Rigidbody2D>().AddForce(new Vector3(Random.Range(bombForceLowerX, bombForceUpperX), Random.Range(bombForceLowerY, bombForceUpperY), 0f));
					}
					Instantiate(dropBulletAudio, transform.position, Quaternion.identity);
				}

				//shrink
				StartCoroutine(ScaleLerp(ExpandScale, 1f, ShrinkTime));
				yield return new WaitForSeconds(ShrinkTime);
				StopCoroutine("ScaleLerp");
			}
		}
	}

	IEnumerator ScaleLerp(float init, float final, float time)
	{
		float i = 0f;
		while (i <= 1)
		{
			i += Time.deltaTime / time;
			float newScale = Mathf.Lerp(init, final, i);
			transform.localScale = new Vector3(newScale, newScale);
			yield return null;
		}
	}

	IEnumerator RotateBack()
	{
		float i = 0f;
		while (i <= 1)
		{
			i += Time.deltaTime / RotateBackTime;
			Quaternion newQua = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0f, 0f, 0f), i);
			transform.rotation = newQua;
			yield return null;
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
