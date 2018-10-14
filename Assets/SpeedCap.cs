using UnityEngine;
using System.Collections;

public class SpeedCap : MonoBehaviour {

	public float capX;

	private Rigidbody2D rb { get { return GetComponent<Rigidbody2D> (); } }

	void FixedUpdate()
	{
		if (Mathf.Abs (rb.velocity.x) > capX)
			rb.velocity = new Vector2 (capX, rb.velocity.y);
	}
}
