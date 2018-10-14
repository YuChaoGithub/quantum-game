using UnityEngine;
using System.Collections;

public class Bromine : Boss
{
	public GameObject beingHitAudio;
	public GameObject blinkAudio;
	public GameObject swimAudio;
	public float leftBound;
	public float rightBound;
	public float upperBound;
	public float lowerBound;
	public GameObject[] fireMonsters;
	public GameObject[] firePositions;
	private const float RotationTime = 0.75f;
	private const float MovementSpeed = 10f;
	private bool moving = false;
	private const float shootInterval = 5f;
	private float timeStamp;
	private float blinkTimer;
	private const float BlinkTime = 1f;

	protected override void Start ()
	{
		base.Start ();
		blinkTimer = timeStamp = Time.timeSinceLevelLoad;
	}

	void Update()
	{
		if (!moving && Time.timeSinceLevelLoad - timeStamp >= shootInterval)
		{
			timeStamp = Time.timeSinceLevelLoad;
			Instantiate(fireMonsters[Random.Range(0, fireMonsters.Length)], firePositions[Random.Range(0, firePositions.Length)].transform.position, Quaternion.identity);
		}

		if (!moving && Time.timeSinceLevelLoad - blinkTimer >= BlinkTime)
		{
			blinkTimer = Time.timeSinceLevelLoad;
			Instantiate(blinkAudio, transform.position, Quaternion.identity);
		}
	}

	protected override void Die ()
	{
		base.Die ();

		base.bottle.transform.position = transform.position;
		Destroy(gameObject);
	}

	public override void TakeDamage (int damage)
	{
		base.TakeDamage(damage);

		if (moving) return;
		Instantiate(beingHitAudio, transform.position, Quaternion.identity);
		StartCoroutine(DamageAnimation());
	}

	IEnumerator DamageAnimation()
	{
		moving = true;
		base.sprite.GetComponent<Animator>().SetBool("Rotate", true);
		Vector3 newPos = new Vector3(Random.Range(leftBound, rightBound), Random.Range(lowerBound, upperBound), 0f);
		float moveTime = Vector3.Distance(transform.position, newPos) / MovementSpeed;
		float newDegree = -90f + Mathf.Rad2Deg * (Mathf.Atan((newPos.y - transform.position.y) / (newPos.x - transform.position.x)));
		//if (newDegree < 0f) newDegree += 360f;
		StartCoroutine(RotateTo(0f, newDegree, RotationTime)); 
		yield return new WaitForSeconds(RotationTime);
		StopCoroutine("RotateTo");
		StartCoroutine(MoveToObjective(transform.position, newPos, moveTime));
		Instantiate(swimAudio, transform.position, Quaternion.identity);
		yield return new WaitForSeconds(moveTime);
		StopCoroutine("MoveToObjective");
		StartCoroutine(RotateTo(newDegree, 0f, RotationTime));
		yield return new WaitForSeconds(RotationTime);
		StopCoroutine("RotateTo");
		base.sprite.GetComponent<Animator>().SetBool("Rotate", false);
		moving = false;
		blinkTimer = Time.timeSinceLevelLoad;
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

	IEnumerator RotateTo(float init, float final, float time)
	{
		float i = 0f;
		while (i <= 1)
		{
			i += Time.deltaTime / time;
			float newRot = Mathf.Lerp(init, final, i);
			transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, newRot));
			yield return null;
		}
	}
}
