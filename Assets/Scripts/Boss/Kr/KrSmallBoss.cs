using UnityEngine;
using System.Collections;

public class KrSmallBoss : MonoBehaviour
{
	public int health;
	public float damage;
	public float upperBound;
	public float lowerBound;
	public float leftBound;
	public float rightBound;
	public GameObject dieExplodePos;
	public GameObject dieExplodePrefab;
	public GameObject dieExplodeArea;
	public float movementSpeed;
	public GameObject appearBlock;
	private bool tinting = false;
	private const float TintDuration = 0.1f;

	void Start()
	{
		StartCoroutine(Movement());
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Player")
		{
			col.gameObject.GetComponent<Player>().Hurt(damage);
		}
	}

	public void BeingHit()
	{
		if (health == 0) Die();
		else health--;

		if (!tinting) Tint();
	}

	void Tint()
	{
		tinting = true;
		GetComponent<SpriteRenderer>().color = Color.red;

		Invoke("RecoverTint", TintDuration);
	}

	void RecoverTint()
	{
		tinting = false;
		GetComponent<SpriteRenderer>().color = Color.white;
	}

	void Die()
	{
		Instantiate(dieExplodeArea, dieExplodePos.transform.position, Quaternion.identity);
		Instantiate(dieExplodePrefab, dieExplodePos.transform.position, Quaternion.identity);
		Destroy(gameObject);
		appearBlock.SetActive(true);
	}

	IEnumerator Movement()
	{
		while (true)
		{
			Vector3 newPos = new Vector3(Random.Range(leftBound, rightBound), Random.Range(lowerBound, upperBound),0f);
			float time = Vector3.Distance(transform.position, newPos) / movementSpeed;
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
