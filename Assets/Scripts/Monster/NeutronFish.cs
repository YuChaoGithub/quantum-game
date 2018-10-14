using UnityEngine;
using System.Collections;

public class NeutronFish : FloatEnemy
{
	public Vector3 goal;
	public float moveTime = 2f;
	public float stopTime = 1f;
	public GameObject[] balls;
	public GameObject[] spawnPos;
	public float ballForceRange;

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
			yield return new WaitForSeconds(stopTime);
			
			//back to initial spot
			ChangeSide();

			i = 0f;
			while (i < 1f)
			{
				i += Time.deltaTime / moveTime;
				Vector3 newPos = Vector3.Lerp(goal, initialPosition, i);
				transform.position = newPos;
				yield return null;
			}
			
			//reached initial spot
			yield return new WaitForSeconds(stopTime);
			ChangeSide();
		}
	}

	void ChangeSide()
	{
		Vector3 scale = transform.localScale;
		scale.x *= -1;
		transform.localScale = scale;
	}

	public override void BeingHit()
	{
		for (int i = 0; i < balls.Length; i++)
		{
			GameObject ball = (Instantiate(balls[i], spawnPos[i].transform.position, Quaternion.identity)) as GameObject;
			ball.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-ballForceRange, ballForceRange), Random.Range(0f, ballForceRange)));
		}

		Destroy(gameObject);
	}
}
