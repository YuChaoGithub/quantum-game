using UnityEngine;
using System.Collections;

public class Arsenic : Boss 
{
	public GameObject beingHitAudio;
	public GameObject spawnAudio;
	public GameObject bombPrefab;
	public GameObject dropPos;
	public float upperBound;
	public float leftBound;
	public float rightBound;
	public float movementSpeed;
	public float risingSpeed;
	public GameObject playerPos;
	private const float FallingChance = 0.5f;
	private bool tinting = false;
	private const float TintDuration = 0.1f;
	private const float FallDelay = 2f;

	protected override void Start ()
	{
		base.Start ();

		StartCoroutine(Cycle());
	}

	IEnumerator Cycle()
	{
		while (base.health > 0f)
		{
			// move horizontally to a location
			Vector3 newPos = new Vector3(Random.Range(leftBound, rightBound), upperBound, 0f);
			float time = Vector3.Distance(transform.position, newPos) / movementSpeed;
			StartCoroutine(MoveToObjective(transform.position, newPos, time));
			yield return new WaitForSeconds(time);
			StopCoroutine("MoveToObjective");

			//drop bomb
			GameObject bomb = (Instantiate(bombPrefab, dropPos.transform.position, Quaternion.identity) as GameObject);
			bomb.GetComponent<Rigidbody2D>().isKinematic = false;
			Instantiate(spawnAudio, transform.position, Quaternion.identity);

			if (Random.value > FallingChance) continue;

			//fall
			newPos = new Vector3(playerPos.transform.position.x, upperBound, 0f);
			time = Vector3.Distance(transform.position, newPos) / (movementSpeed*2f);
			StartCoroutine(MoveToObjective(transform.position, newPos, time));
			yield return new WaitForSeconds(time);
			StopCoroutine("MoveToObjective");
			sprite.GetComponent<SpriteRenderer>().color = Color.black;
			GetComponent<Rigidbody2D>().isKinematic = false;

			//rise
			yield return new WaitForSeconds(FallDelay);
			sprite.GetComponent<SpriteRenderer>().color = Color.white;
			newPos = new Vector3(transform.position.x, upperBound, 0f);
			time = Vector3.Distance(transform.position, newPos) / risingSpeed;
			StartCoroutine(MoveToObjective(transform.position, newPos, time));
			yield return new WaitForSeconds(time);
			StopCoroutine("MoveToObjective");
		}
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
