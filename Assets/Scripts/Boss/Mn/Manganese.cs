using UnityEngine;
using System.Collections;

public class Manganese : Boss
{
	public Sprite[] sprites;
	public GameObject[] debris;
	public GameObject[] fallPos;
	private const float UpperBound = 18f;
	private const float RightBound = 244.5f;
	private const float LeftBound = 214.5f;
	private const float LowerBound = 8.5f;
	private const float MovementSpeed = 7f;

	protected override void Start ()
	{
		base.Start ();

		StartCoroutine(Movement());
	}

	public override void TakeDamage (int damage)
	{
		if (base.health == 0) return;

		base.TakeDamage (damage);

		foreach (GameObject pos in fallPos)
		{
			Instantiate(debris[Random.Range(0, debris.Length)], pos.transform.position, Quaternion.identity);
		}
	
		base.sprite.GetComponent<SpriteRenderer>().sprite = sprites[Mathf.RoundToInt(Mathf.Lerp(0f, (float)(sprites.Length - 1), 1 - ((health > 0 ? health : 0) / FullHealth)))];
	}

	protected override void Die ()
	{
		base.Die ();

		base.bottle.transform.position = transform.position;

		foreach (GameObject pos in fallPos)
		{
			Instantiate(debris[Random.Range(0, debris.Length)], pos.transform.position, Quaternion.identity);
		}

		Destroy(gameObject);
	}

	IEnumerator Movement()
	{
		while (base.health > 0f)
		{
			Vector3 newPos = new Vector3(Random.Range(LeftBound, RightBound), Random.Range(LowerBound, UpperBound), -0.1f);
			float time = Vector3.Distance(transform.position, newPos) / MovementSpeed;
			StartCoroutine(MoveToObjective(transform.position, newPos, time));
			yield return new WaitForSeconds(time);
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
