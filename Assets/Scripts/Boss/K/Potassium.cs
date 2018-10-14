using UnityEngine;
using System.Collections;

public class Potassium : Boss
{
	public GameObject[] dropPoses;
	public GameObject[] stonePrefabs;

	public GameObject dieAudio;
	public GameObject fallAudio;

	private bool tinting = false;
	private const float TintDuration = 0.1f;
	private const float TopLine = 38.5f;
	private const float BottomLine = 32f;
	private const float LeftBound = 212.5f;
	private const float RightBound = 243.5f;
	private const float RisingSpeed = 3f;
	private const float FallingSpeed = 20f;
	private const float DropStoneDelayTime = 1f;
	private const float MovementSpeed = 4f;

	protected override void Start ()
	{
		base.Start ();

		StartCoroutine(Cycle());
	}

	public override void TakeDamage(int damage)
	{
		if (health == 0) return;

		base.TakeDamage(damage);

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

	IEnumerator Cycle()
	{
		while (base.health > 0f)
		{
			//go to top line
			Vector3 newPos = new Vector3(transform.position.x, TopLine, 0f);
			float time = Vector3.Distance(transform.position, newPos) / RisingSpeed;
			StartCoroutine(MoveToObjective(transform.position, newPos, time));
			yield return new WaitForSeconds(time);
			StopCoroutine("MoveToObjective");

			//horizontal movement
			newPos = new Vector3(Random.Range(LeftBound, RightBound), TopLine, 0f);
			time = Vector3.Distance(transform.position, newPos) / MovementSpeed;
			StartCoroutine(MoveToObjective(transform.position, newPos, time));
			yield return new WaitForSeconds(time);
			StopCoroutine("MoveToObjective");

			//drop stones
			yield return new WaitForSeconds(DropStoneDelayTime);
			DropStones();
			yield return new WaitForSeconds(DropStoneDelayTime);

			//horizontal movement
			newPos = new Vector3(Random.Range(LeftBound, RightBound), TopLine, 0f);
			time = Vector3.Distance(transform.position, newPos) / MovementSpeed;
			StartCoroutine(MoveToObjective(transform.position, newPos, time));
			yield return new WaitForSeconds(time);
			StopCoroutine("MoveToObjective");

			//fall
			newPos = new Vector3(transform.position.x, BottomLine, 0f);
			time = Vector3.Distance(transform.position, newPos) / FallingSpeed;
			StartCoroutine(MoveToObjective(transform.position, newPos, time));
			Instantiate(fallAudio, transform.position, Quaternion.identity);
			yield return new WaitForSeconds(time + DropStoneDelayTime);
			StopCoroutine("MoveToObjective");
			DropStones();
			yield return new WaitForSeconds(DropStoneDelayTime * 2);
		}
	}

	void DropStones()
	{
		foreach (GameObject dropPos in dropPoses)
		{
			Instantiate(stonePrefabs[Random.Range(0, stonePrefabs.Length)], dropPos.transform.position, Quaternion.identity);
		}
		Instantiate(fallAudio, transform.position, Quaternion.identity);
	}

	protected override void Die ()
	{
		base.Die ();

		base.bottle.transform.position = transform.position;
		Instantiate(dieAudio, transform.position, Quaternion.identity);
		DropStones();
		Destroy(gameObject);
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
