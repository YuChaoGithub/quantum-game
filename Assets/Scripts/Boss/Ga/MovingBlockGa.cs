using UnityEngine;
using System.Collections;

public class MovingBlockGa : MonoBehaviour 
{
	public Vector3 goal;
	public float moveTime = 2f;
	public float stopTime = 1f;
	public GameObject audio;
	private const float AnimationInterval = 0.5f;

	private Vector3 initialPosition;

	void Start()
	{
		initialPosition = transform.position;
		StartCoroutine(movement());
	}

	IEnumerator movement()
	{
		float i;

		while (true)
		{
			InvokeRepeating("PlaySound", 0f, AnimationInterval);
			//move to goal
			i = 0f;
			while (i < 1f)
			{
				i += Time.deltaTime / moveTime;
				Vector3 newPos = Vector3.Lerp(initialPosition, goal, i);
				transform.position = newPos;
				yield return null;
			}

			//reached goal
			CancelInvoke("PlaySound");
			GetComponent<Animator>().SetTrigger("ChangeDir");
			InvokeRepeating("PlaySound", 0f, AnimationInterval);
			yield return new WaitForSeconds(stopTime);


			//back to initial spot
			i = 0f;
			while (i < 1f)
			{
				i += Time.deltaTime / moveTime;
				Vector3 newPos = Vector3.Lerp(goal, initialPosition, i);
				transform.position = newPos;
				yield return null;
			}

			//reached initial spot
			CancelInvoke("PlaySound");
			GetComponent<Animator>().SetTrigger("ChangeDir");
			InvokeRepeating("PlaySound", 0f, AnimationInterval);
			yield return new WaitForSeconds(stopTime);
		}

	}

	void PlaySound()
	{
		Instantiate(audio, transform.position, Quaternion.identity);
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Player" || col.gameObject.tag == "Monster")
		{
			col.transform.parent = gameObject.transform;
		}
	}

	void OnTriggerExit2D(Collider2D col)
	{
		if (col.gameObject.tag == "Player" || col.gameObject.tag == "Monster")
		{
			col.transform.parent = null;
		}
	}
}
