using UnityEngine;
using System.Collections;

public class Aluminium : Boss
{
	public GameObject burstPosition;
	public GameObject burstPrefab;
	public GameObject beingHitAudio;
	public GameObject burstAudio;
	public GameObject dropAudio;
	private const float LeftBound = 214f;
	private const float RightBound = 232f;
	private const float TopLine = 15f;
	private const float MoveToObjectiveSpeed = 6f;
	private const float FloatingSpeed = 3f;
	private const float FallingDelay = 0.5f;
	private const float OnFloorTime = 1.5f;
	private const float BurstAnimationTime = 0.583f;
	private const float StopInAirTime = 0.5f;
	private const float DieAnimationTime = 2f;
	private const float DiePosY = -2.6f;

	protected override void Start ()
	{
		base.Start ();

		StartCoroutine(Cycle());
	}

	public override void TakeDamage (int damage)
	{
		base.TakeDamage (damage);

		Instantiate(beingHitAudio, transform.position, Quaternion.identity);
	}

	protected override void Die ()
	{
		base.Die ();

		base.bottle.transform.position = new Vector3(transform.position.x, TopLine, 0f);
		StopAllCoroutines();
		StartCoroutine(MoveToObjective(transform.position, new Vector3(transform.position.x, DiePosY, 0f), DieAnimationTime));
		Invoke("Remove", DieAnimationTime);
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "Ground")
			Instantiate(dropAudio, transform.position, Quaternion.identity);
	}

	void Remove()
	{
		Destroy(gameObject);
	}

	IEnumerator Cycle()
	{
		while (base.health > 0f) 
		{
			//go to objective on top line
			Vector3 newPos = new Vector3(Random.Range(LeftBound, RightBound), TopLine, 0f);
			float time = Vector3.Distance(transform.position, newPos) / MoveToObjectiveSpeed;
			StartCoroutine(MoveToObjective(transform.position, newPos, time));
			yield return new WaitForSeconds(time);
			StopCoroutine("MoveToObjective");
			yield return new WaitForSeconds(StopInAirTime);

			//fall
			GetComponent<Rigidbody2D>().isKinematic = false;
			yield return new WaitForSeconds(FallingDelay);

			//burst
			base.sprite.GetComponent<Animator>().SetTrigger("Shoot");
			Instantiate(burstAudio, transform.position, Quaternion.identity);
			yield return new WaitForSeconds(BurstAnimationTime);
			Instantiate(burstPrefab, burstPosition.transform.position, Quaternion.identity);
			yield return new WaitForSeconds(OnFloorTime);

			//float to top line
			GetComponent<Rigidbody2D>().isKinematic = true;
			Vector3 topPos = new Vector3(transform.position.x, TopLine, 0f);
			float toTopTime = Vector3.Distance(transform.position, topPos) / FloatingSpeed;
			StartCoroutine(MoveToObjective(transform.position, topPos, toTopTime));
			yield return new WaitForSeconds(toTopTime);
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
