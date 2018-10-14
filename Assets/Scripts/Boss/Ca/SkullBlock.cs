using UnityEngine;
using System.Collections;

public class SkullBlock : MonoBehaviour 
{

	public float fallTime = 3f;
	public float goalY;
	public GameObject audio;

	private bool isFalling = false;
	private Vector3 initialPos;

	private const float WaitTime = 0.25f;
	private const float RestoreTime = 1f;

	void Start()
	{
		initialPos = transform.position;
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Player")
		{
			col.transform.parent = gameObject.transform;

			//start movement
			if (!isFalling)
			{
				StartCoroutine(Fall());
			}
		}
	}

	void OnTriggerExit2D(Collider2D col)
	{
		if (col.gameObject.tag == "Player")
		{
			col.transform.parent = null;
		}
	}

	IEnumerator Fall()
	{
		//chang eye color to red
		GetComponent<Animator>().SetBool("fall", true);
		Instantiate(audio, transform.position, Quaternion.identity);
		isFalling = true;

		yield return new WaitForSeconds(WaitTime);

		//movement
		float i = 0f;
		float scale = 1f / fallTime;
		while (i < 1f)
		{
			i += Time.deltaTime * scale;

			//set position
			float newY = Mathf.Lerp(initialPos.y, goalY, i);
			transform.position = new Vector3(initialPos.x, newY);

			//set alpha
			Color color = GetComponent<SpriteRenderer>().color;
			color.a = 1f - i;
			GetComponent<SpriteRenderer>().color = color;

			yield return null;
		}

		//get rid of the collider
		GetComponent<PolygonCollider2D>().enabled = false;

		yield return new WaitForSeconds(RestoreTime);

		//restore
		StopCoroutine("Fall");
		Restore();
	}

	void Restore()
	{
		isFalling = false;

		//restore eye color
		GetComponent<Animator>().SetBool("fall", false);

		//restore position
		transform.position = new Vector3(initialPos.x, initialPos.y);

		//restore alpha
		Color color = GetComponent<SpriteRenderer>().color;
		color.a = 1f;
		GetComponent<SpriteRenderer>().color = color;

		//restore collider
		GetComponent<PolygonCollider2D>().enabled = true;
	}
}
