using UnityEngine;
using System.Collections;

public class StopAndChangeSide : MonoBehaviour 
{
	public float flyingDurationUntilSideChange;
	public float stopTime;

	private float speed;
	private float side;

	void Start()
	{
		speed = GetComponent<Bullet>().speed;
		side = GetComponent<Bullet>().side;
		StartCoroutine(Movement());
	}

	IEnumerator Movement()
	{
		while (true)
		{
			yield return new WaitForSeconds(flyingDurationUntilSideChange);
			GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
			yield return new WaitForSeconds(stopTime);
			GetComponent<Rigidbody2D>().velocity = new Vector2(-1f * side * speed, 0f);
		}
	}
}
